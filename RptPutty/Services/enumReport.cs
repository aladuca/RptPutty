using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using RptPutty.Models;
using System.Configuration;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportAppServer.DataDefModel;

namespace RptPutty.Services
{
    public class enumReport
    {
        public Report RunDefinition()
        {
            return RunDefinition(ConfigurationManager.AppSettings["defaultReport"]);

        }
        public Report RunDefinition(string filename)
        {
            Report rptDef = new Report();
            rptDef.Filename = ConfigurationManager.AppSettings["searchPath"] + filename;
            

            ReportDocument rptDoc = new ReportDocument();

            try { rptDoc.Load(rptDef.Filename); }
            catch { return null; }

            if (!String.IsNullOrEmpty(rptDoc.SummaryInfo.ReportTitle))
                rptDef.Title = rptDoc.SummaryInfo.ReportTitle;
            else
                rptDef.Title = Path.GetFileName(rptDef.Filename);

            // Populate Default Parameter Values
            Dictionary<string, string> defaultvals = new Dictionary<string, string>();
            foreach (ISCRParameterField prms in rptDoc.ReportClientDocument.DataDefinition.ParameterFields)
            {
                ISCRValues vals = prms.InitialValues;

                foreach (ParameterFieldDiscreteValue val in vals)
                {
                    if (!String.IsNullOrEmpty(val.Value.ToString()))
                        defaultvals.Add(prms.Name, val.Value.ToString());
                }
            }
            // Populate Parameter Details
            foreach (ParameterFieldDefinition prm in rptDoc.DataDefinition.ParameterFields)
            {
                Parameters param = new Parameters();
                if (!prm.IsLinked() && !prm.ParameterFieldUsage2.ToString().Equals("NotInUse"))
                {
                    param.Name = prm.Name;
                    param.PromptText = prm.PromptText;
                    param.MultipleSelect = prm.EnableAllowMultipleValue;
                    param.AllowCustomValues = rptDoc.ParameterFields[prm.Name, prm.ReportName].AllowCustomValues;
                    if (defaultvals.ContainsKey(prm.Name)) { param.DefaultValue = defaultvals[prm.Name]; }
                    ParameterValues crpvs = prm.DefaultValues;
                    foreach (ParameterValue crpv in crpvs)
                    {
                        ParameterDiscreteValue crpdv = (ParameterDiscreteValue)crpv;
                        if (crpdv.Description != null)
                        {
                            param.DiscreteValues.Add(new Option(crpdv.Value.ToString(), crpdv.Description));
                        }
                        else
                        {
                            param.DiscreteValues.Add(new Option(crpdv.Value.ToString(), crpdv.Value.ToString()));
                        }
                    }
                    rptDef.Parameters.Add(param);
                }
            }
            rptDoc.Close();
            rptDoc.Dispose();
            return rptDef;
        }
        public Listing getSummary(Listing rpt)
        {
            ReportDocument rptDoc = new ReportDocument();
            try { rptDoc.Load(ConfigurationManager.AppSettings["searchPath"] + rpt.Filename); }
            catch { return rpt; }
            rpt.Title = rptDoc.SummaryInfo.ReportTitle;
            rpt.Comments = rptDoc.SummaryInfo.ReportComments;

            rptDoc.Close();
            rptDoc.Dispose();
            return rpt;

        }
    }
}