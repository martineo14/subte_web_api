using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using WebApiSubte.Models;

namespace WebApiSubte.Utils
{
  public class HtmlParser
  {
  }

  static class EstadoLineaFinder
  {
    public static List<LineaSubteModel> Find(string file)
    {
      List<LineaSubteModel> list = new List<LineaSubteModel>();

      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(file);
      var nodes = doc.DocumentNode.Descendants("span").Where(div => div.Id.Contains("status-line"));
      foreach (var node in nodes)
      {
        LineaSubteModel i = new LineaSubteModel();
        i.Id = node.Id.Substring(node.Id.Length - 1, 1);
        i.Name= node.InnerText;
        list.Add(i);
      }
      return list;
    }
  }
}