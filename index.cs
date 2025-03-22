using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace MangoWebPoolService.Areas.Report.Models.Any.ICE
{
  public class v_ice_rpt_eiei
  {
    public Authorize auth;
    public decimal? tmp_amtbal3 = (decimal)0;

    private ObjectMemCache cache;

    public v_ice_rpt_eiei(Authorize auth)
    {
      this.auth = auth;
      cache = new ObjectMemCache();
    }
    /* ------------------------------------------------------------------------------ */

    public Tuple<dynamic, string> Load_Report(List<ReportCondition> cond, string icdate,string pre_event2)
    {
      dynamic data = null;
      string err = "";
      using (var db = new DataContext())
      {
        var AsToDate = Dtl.parse_date(icdate)?.ToString("yyyy-MM-dd");

        DataExtensions.TryCatchExecute(() =>
        {
          var condition = ReportGenarator.FilterReportSQL(cond, pre_cond: "and");

          var pre_event = cond.Where(x => x.field_name == "a.pre_event").Select(x => x.value?.ToString()).FirstOrDefault();
        //  var pre_event2 = MGF.GetPreEvent2(db, auth, pre_event);

          //  throw new Exception(AsToDate.ToString());
     
     
     

          // throw new Exception(sql);


         var sql =$@"";
          var q = db.SqlFetch2<Dictionary<string, object>>(sql, 0, null);

          var cc_code = q.Select(g => g[""]?.ToString()).FirstOrDefault();
          var c_code0 = q.Select(g => g[""]?.ToString()).FirstOrDefault();

          var sql_d = $@"";

          var q2 = db.SqlFetch2<Dictionary<string, object>>(sql_d, 0, null);

          var rankDay = GroupOfDay(q2);

          var group = GroupInformation(q, rankDay);

          var lr_data = ReportGenarator.ReformatDataGrid(cond, group);

          var dir_raw_data = q.ToList();
          string files = "";
          if (dir_raw_data.Count > 0)
          {
            files = cache.AddCache(dir_raw_data, 1);
          }

          var grand = grandTotal(q, rankDay);

          data = new
          {
            data = lr_data,
            data2 = q2,
            rankDay = rankDay,
            grand_total = grand,
            raw_data = new List<string>(),
            total = lr_data.Count,
            raw_files = files,
           // qq = sql,
            group = group


          };

        }, ref err);
      }
      return new Tuple<dynamic, string>(data, err);
    }


    public class Models
    {
      public string refcode { get; set; }
      public string text_group1 { get; set; }
      public string text_group2 { get; set; }
      public string cc_code { get; set; }
      public string c_code0 { get; set; }
      public string prod_gname { get; set; }
      public string row_no { get; set; }
      public string itemcode { get; set; }
      public string itemname { get; set; }
      public string unitname { get; set; }
      public string docno { get; set; }
      public DateTime? icdate { get; set; }
      public string doctype_n { get; set; }
      public string serialnumber { get; set; }
      public string whname { get; set; }
      public decimal? qtybal { get; set; }
      public decimal? amtbal { get; set; }
      public decimal? daydiff { get; set; }
      public string day_fr { get; set; }
      public string day_to { get; set; }

    }
  }
}