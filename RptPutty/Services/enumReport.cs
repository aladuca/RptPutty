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
        public ReportForm RunDefinition()
        {
            return RunDefinition(ConfigurationManager.AppSettings["defaultReport"]);
            
        }
        public ReportForm RunDefinition(string filename)
        {
            ReportForm rptDef = new ReportForm();
            rptDef.Filename = ConfigurationManager.AppSettings["searchPath"] + filename;
            //rptDef.Parameters = new List<Parameters>();

            ReportDocument rptDoc = new ReportDocument();

            try { rptDoc.Load(rptDef.Filename); }
            catch { return null; }

            foreach (ParameterFieldDefinition prm in rptDoc.DataDefinition.ParameterFields)
            {
                Parameters param = new Parameters();
                if (!prm.IsLinked() && !prm.ParameterFieldUsage2.ToString().Equals("NotInUse"))
                {
                    param.Name = prm.Name;
                    param.MultipleSelect = prm.EnableAllowMultipleValue;
                    param.AllowCustomValues = rptDoc.ParameterFields[prm.Name, prm.ReportName].AllowCustomValues;
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

            return rptDef;
        }
    }
}