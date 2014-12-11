using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            Dictionary<string, string> defaultvals = new Dictionary<string, string>();
            foreach (ISCRParameterField prms in rptDoc.ReportClientDocument.DataDefinition.ParameterFields)
            {
                ISCRValues vals = prms.InitialValues;

                foreach (ParameterFieldDiscreteValue val in vals)
                {
                    defaultvals.Add(prms.Name, val.Value);
                }
            }
            foreach (ParameterFieldDefinition prm in rptDoc.DataDefinition.ParameterFields)
            {
                Parameters param = new Parameters();
                if (!prm.IsLinked() && !prm.ParameterFieldUsage2.ToString().Equals("NotInUse"))
                {
                    param.Name = prm.Name;
                    param.MultipleSelect = prm.EnableAllowMultipleValue;
                    param.AllowCustomValues = rptDoc.ParameterFields[prm.Name, prm.ReportName].AllowCustomValues;
                    param.DefaultValue = defaultvals[prm.Name];
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
                }
                rptDef.Parameters.Add(param);
            }
            rptDoc.Close();
            rptDoc.Dispose();
            return rptDef;
        }
    }
}