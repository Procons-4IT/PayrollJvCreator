namespace PayRollEntryApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;

    public class B1Helper
    {
        #region VARIABLES
        private static SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
        public static SAPbobsCOM.Company DiCompany
        {
            get
            {
                if (!oCompany.Connected)
                {
                    ConnectCompany(oCompany, true);
                    Logger.LogToFile("\n Company Connected...");
                }
                return oCompany;
            }
        }
        #endregion
        public static void ConnectCompany(SAPbobsCOM.Company oCompany, bool isBaseCompany)
        {
            Trace.WriteLine(string.Concat("[DR] Connect to company starts..."));

            int i = isBaseCompany ? 0 : 6;
            var appSetting = System.Configuration.ConfigurationManager.AppSettings;

            oCompany.Server = appSetting["SapServer"];
            oCompany.LicenseServer = appSetting["LicenseServer"];
            string DbServerType = appSetting["DBType"];
            SAPbobsCOM.BoDataServerTypes dbServerType;
            switch (DbServerType)
            {
                case "2005": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005; break;
                case "2008": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008; break;
                case "2012": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012; break;
                case "2014": dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014; break;
                default: dbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB; break;

            }
            oCompany.DbServerType = dbServerType;
            oCompany.CompanyDB = appSetting["CompanyDB"];
            oCompany.UserName = appSetting["UserName"];
            oCompany.Password = appSetting["Password"];
            oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ExportImportMode;

            int checkConnected = oCompany.Connect();
            if (checkConnected != 0)
            {
                string message = string.Format("Error: Could not connect to Company {0}.{1}", oCompany.CompanyDB, oCompany.GetLastErrorDescription());
                
                Trace.WriteLine(message);
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("[DR] Company connected...");
            }
        }
        public static int CreateJournalEntry(List<JVObject> jvObjectList, RichTextBox txtOutput)
        {
            Logger.LogToFile("\n Start importing journal entries...");
            Trace.WriteLine(string.Concat("[DR] CreateJournalEntry starts..."));
            var journalEntry = DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries) as SAPbobsCOM.JournalEntries;
            var oService = DiCompany.GetCompanyService();
            var oBusinessService = oService.GetBusinessService(SAPbobsCOM.ServiceTypes.ProfitCentersService) as SAPbobsCOM.IProfitCentersService;
            var pCenterParams = oBusinessService.GetDataInterface(SAPbobsCOM.ProfitCentersServiceDataInterfaces.pcsProfitCenterParams) as SAPbobsCOM.ProfitCenterParams;

            try
            {
                journalEntry.ReferenceDate = jvObjectList.Min(x => x.PayrollDate);
                journalEntry.DueDate = jvObjectList.Max(x => x.PayrollDate);

                for (int i = 0; i < jvObjectList.Count; i++)
                {
                    if (i > 0)
                        journalEntry.Lines.Add();

                    pCenterParams.CenterCode = jvObjectList[i].CostCenter;
                    var pcenter = oBusinessService.GetProfitCenter(pCenterParams);

                    switch (pcenter.InWhichDimension)
                    {
                        case 1: journalEntry.Lines.CostingCode = jvObjectList[i].CostCenter; break;
                        case 2: journalEntry.Lines.CostingCode2 = jvObjectList[i].CostCenter; break;
                        case 3: journalEntry.Lines.CostingCode3 = jvObjectList[i].CostCenter; break;
                        case 4: journalEntry.Lines.CostingCode4 = jvObjectList[i].CostCenter; break;
                        case 5: journalEntry.Lines.CostingCode5 = jvObjectList[i].CostCenter; break;
                    }

                    journalEntry.Lines.Debit = jvObjectList[i].Debit;
                    journalEntry.Lines.Credit = jvObjectList[i].Credit;
                    journalEntry.Lines.AccountCode = jvObjectList[i].GLAccount;
                    journalEntry.Lines.ReferenceDate1 = jvObjectList[i].PayrollDate;
                    txtOutput.AppendText(string.Format("\n Importing CostCenter: {0}, Dedit: {1}, Credit: {2}, Account: {3}, Date: {4} ...", jvObjectList[i].CostCenter, jvObjectList[i].Debit, jvObjectList[i].Credit, jvObjectList[i].GLAccount, jvObjectList[i].PayrollDate.ToShortDateString()));
                    Logger.LogToFile(string.Format("\n Importing CostCenter: {0}, Dedit: {1}, Credit: {2}, Account: {3}, Date: {4} ...", jvObjectList[i].CostCenter, jvObjectList[i].Debit, jvObjectList[i].Credit, jvObjectList[i].GLAccount, jvObjectList[i].PayrollDate.ToShortDateString()));
                    txtOutput.Update();
                }

                if (journalEntry.Add() != 0)
                {
                    var error = DiCompany.GetLastErrorDescription();
                    Trace.WriteLine(string.Concat("[DR] B1 error at CreateJournalEntryLC: ", error));
                    return 0;
                }
                else
                {
                    Logger.LogToFile(string.Concat("Import completed with document numer: ", DiCompany.GetNewObjectKey()));
                    return int.Parse(DiCompany.GetNewObjectKey());
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Concat("[DR] Exception at CreateJournalEntryLC: ", ex.Message));
                txtOutput.AppendText(string.Concat("Exception at CreateJournalEntryLC: ", ex.Message));
                Logger.LogToFile(string.Concat("Exception at CreateJournalEntryLC: ", ex.Message));
                txtOutput.Update();
                return 0;
            }
            finally
            {
                journalEntry.ReleaseObject();
                oService.ReleaseObject();
            }
        }

    }
}
