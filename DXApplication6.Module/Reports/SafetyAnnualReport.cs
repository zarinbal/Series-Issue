using DevExpress.CodeParser;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TES.Module.Modules.CollisionMod.Reports.AnnualReport;

namespace AnnualReport
{
    [DisplayName("Annual Road Safety Report")]
    [DomainComponent]
    [VisibleInReports]
    public class SafetyAnnualReport
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public float Duration => EndYear - StartYear + 1;
        public string ReportDuration
        {
            get
            {
                if (StartYear == EndYear)
                {
                    return string.Format("1 Year ({0})", StartYear);
                }
                else
                {
                    return string.Format("{0} Years ({1}-{2})", Duration, StartYear, EndYear);
                }
            }
        }

        //public List<CollisionNew> Collisions { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> CollisionsByMonth { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> CollisionsByDay { get; set; }
        public List<ReportEntryExtended<DateTime, double, string, bool>> CollisionsByHour { get; set; }

        public List<ReportEntryExtended<string, double, CollisionPedCyclistInvolvement, int>> PedestrianCyclistByDayOfWeek { get; set; }
        public List<ReportEntryExtended<DateTime, double, CollisionPedCyclistInvolvement, bool>> PedestrianCyclistByTimeOfDay { get; set; }
        public List<ReportEntry<string, double, CollisionPedCyclistInvolvement>> PedestrianCyclistByMonth { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> SpeedRelatedCollisionsByMonth { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> SpeedRelatedCollisionsByDay { get; set; }
        public List<ReportEntryExtended<DateTime, double, string, bool>> SpeedRelatedCollisionsByHour { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> LostControlCollisionsByMonth { get; set; }
        public List<ReportEntry<ChartArgument<string>, double, string>> LostControlCollisionsByDay { get; set; }
        public List<ReportEntryExtended<DateTime, double, string, bool>> LostControlCollisionsByHour { get; set; }
        public List<ReportEntryExtended<int, double, string, CollisionPedCyclistInvolvement>> PedestrianCyclistByYear { get; set; }
    }



    public enum CollisionPedCyclistInvolvement
    {
        None = 0,
        Cyclist = 1,
        Pedestrian = 2,
        PedestrianCyclist = 3,

    }

    [DomainComponent]
    public class ReportEntryExtended<ArgumentType, ValueType, FilterType, AdditionalInfoType> :
        ReportEntry<ArgumentType, ValueType, FilterType>
    {
        public AdditionalInfoType AdditionalInfo { get; set; }
    }

    [DomainComponent]
    public class ReportEntry<ArgumentType, ValueType, FilterType>
    {
        public ArgumentType Argument { get; set; }
        public ValueType Value { get; set; }
        public FilterType Filter { get; set; }

    }
}
