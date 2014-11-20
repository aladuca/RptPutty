using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RptPutty.Models
{
    public class ReportJob
    {
        public Guid JobID { get; set; }
        public ReportSubmit report { get; set; }
        public Email email { get; set; }
    }
    // Base Report Class Definition
    public class Report
    {
        public string Filename { get; set; }
        public List<Parameters> Parameters { get; set; }
    }
    // Report Class Definition used to present form
    public class ReportForm : Report
    {
        public ReportForm()
        {
            Parameters = new List<Parameters>();
            Output = new Dictionary<int, string>();
            Output.Add(0, "NoFormat");
            Output.Add(1, "CrystalReport");
            Output.Add(2, "RichText");
            Output.Add(3, "WordForWindows");
            Output.Add(4, "Excel");
            Output.Add(5, "PortableDocFormat");
            Output.Add(6, "HTML32");
            Output.Add(7, "HTML40");
            Output.Add(8, "ExcelRecord");
            Output.Add(9, "Text");
            Output.Add(10, "CharacterSeparatedValues");
            Output.Add(11, "TabSeperatedText");
            Output.Add(12, "EditableRTF");
            Output.Add(13, "Xml");
            Output.Add(14, "RPTR");
            Output.Add(15, "ExcelWorkbook");
        }
        public Dictionary<int, string> Output { get; set; }
    }
    public class ReportSubmit : Report
    {
        public Output SelectedOutput { get; set; }
    }
    public class Parameters
    {
        public Parameters()
        {
            DiscreteValues = new List<Option>();
            SelectedValues = new List<string>();
        }
        public string Name { get; set; }
        public bool MultipleSelect { get; set; }
        public bool AllowCustomValues { get; set; }
        public List<Option> DiscreteValues { get; set; }
        public string DefaultValue { get; set; }
        public List<string> SelectedValues { get; set; }
    }
    public class Option
    {
        public Option() { }
        public Option(string val, string desc)
        {
            this.Value = val;
            this.Description = desc;
        }
        public string Value { get; set; }
        public string Description { get; set; }
    }
    public enum Output
    {

        NoFormat = 0,
        CrystalReport = 1,
        RichText = 2,
        WordForWindows = 3,
        Excel = 4,
        PortableDocFormat = 5,
        HTML32 = 6,
        HTML40 = 7,
        ExcelRecord = 8,
        Text = 9,
        CharacterSeparatedValues = 10,
        TabSeperatedText = 11,
        EditableRTF = 12,
        Xml = 13,
        RPTR = 14,
        ExcelWorkbook = 15,
    }
    public class Email
    {
        public Custom custom { get; set; }
        public List<string> to { get; set; }
        public List<string> cc { get; set; }
    }
    public class Custom
    {
        public string subject { get; set; }
        public string body { get; set; }
        public bool supressparameters { get; set; }
    }

}