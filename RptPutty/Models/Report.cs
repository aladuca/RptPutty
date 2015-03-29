using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RptPutty.Models
{
    public class ReportJob
    {
        public Guid JobID { get; set; }
        public Report report { get; set; }
        public Email email { get; set; }
    }
    // Base Report Class Definition
    public class Report
    {
        public Report()
        {
            Parameters = new List<Parameters>();
            Output = new List<Option>();
            Output.Add(new Option("0", "NoFormat"));
            Output.Add(new Option("1", "CrystalReport"));
            Output.Add(new Option("2", "RichText"));
            Output.Add(new Option("3", "WordForWindows"));
            Output.Add(new Option("4", "Excel"));
            Output.Add(new Option("5", "PortableDocFormat"));
            Output.Add(new Option("6", "HTML32"));
            Output.Add(new Option("7", "HTML40"));
            Output.Add(new Option("8", "ExcelRecord"));
            Output.Add(new Option("9", "Text"));
            Output.Add(new Option("10", "CharacterSeparatedValues"));
            Output.Add(new Option("11", "TabSeperatedText"));
            Output.Add(new Option("12", "EditableRTF"));
            Output.Add(new Option("13", "Xml"));
            Output.Add(new Option("14", "RPTR"));
            Output.Add(new Option("15", "ExcelWorkbook"));
        }
        public Guid guid { get; set; }
        public string Filename { get; set; }
        public string Title { get; set; }
        public List<Parameters> Parameters { get; set; }
        public List<Option> Output { get; set; }
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
        public string PromptText { get; set; }
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