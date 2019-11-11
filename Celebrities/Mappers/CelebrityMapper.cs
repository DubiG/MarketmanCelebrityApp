using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Celebrities.Models;
using HtmlAgilityPack;

namespace Celebrities.Mappers
{
    public class CelebrityMapper : IMapper<Celebrity>
    {
        public Celebrity Map(HtmlDocument htmlDocument)
        {
            throw new NotImplementedException();
        }

        public List<Celebrity> MapAsList(HtmlDocument htmlDocument)
        {
            var celebrityList = new List<Celebrity>();
            HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'lister-item mode-detail')]");
            foreach (var item in htmlNodeCollection)
            {
                try
                {
                    Celebrity celebrity = new Celebrity
                    {
                        Id = GetId(item),
                        Name = getName(item),
                        BirthDay = GetBirthday(item),
                        picUrl = GetImageUrl(item),
                        isViewable = true
                    };
                    celebrity.Role = getRole(item);
                    celebrity.Gender = getGender(celebrity.Role, item);

                    celebrityList.Add(celebrity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }
            return celebrityList;
        }

        private string GetBirthday(HtmlNode item)
        {
            IEnumerable<HtmlNode> descriptions = item.SelectSingleNode(".//div[contains(@class, 'lister-item-content')]").Descendants("p");
            Match match = descriptions.Where(d => d.InnerText.ToLower().Contains("born")).Select(d => Regex.Match(d.InnerText.ToLower(), @"\d{4}")).Where(m => !string.IsNullOrEmpty(m.Value)).FirstOrDefault();
            string year = match?.Value;
            int intYear;
            return int.TryParse(year, out intYear) && intYear > 2000 ? null : year;
        }

        private string getGender(string role, HtmlNode item)
        {
            Gender gender = Gender.Male;
            string lowercaseRole = role.ToLower();
            if (lowercaseRole == "actress")
            {
                gender = Gender.Female;
            }
            else if (lowercaseRole != "actor")
            {
                IEnumerable<HtmlNode> descriptions = item.SelectSingleNode(".//div[contains(@class, 'lister-item-content')]").Descendants("p");
                gender = descriptions.Any(d => Regex.IsMatch(d.InnerText.ToLower(), @"\bher\b")) ? Gender.Female : Gender.Male;
            }

            return gender.ToString();
        }

        private string getRole(HtmlNode item)
        {
            HtmlNode itemText = item.SelectSingleNode(".//p[contains(@class, 'text-muted text-small')]");
            int lastIndex = itemText.InnerText.IndexOf('|');
            string role = itemText.InnerText.Substring(0, lastIndex);
            return Regex.Replace(role, @"\t|\n|\r", "").Trim();
        }

        private string getName(HtmlNode item)
        {
            HtmlNode itemHeader = item.SelectSingleNode(".//h3[contains(@class, 'lister-item-header')]");
            HtmlNode itemName = itemHeader.Descendants("a").First();
            return Regex.Replace(itemName.InnerText, @"\t|\n|\r", "").Trim();
        }

        private int GetId(HtmlNode item)
        {
            HtmlNode itemIndex = item.SelectSingleNode(".//span[contains(@class, 'lister-item-index unbold text-primary')]");
            string index = itemIndex.InnerText.Replace('.', ' ');
            int.TryParse(index, out int id);
            return id;
        }

        private static string GetImageUrl(HtmlNode item)
        {
            HtmlNode itemImageNode = item.SelectSingleNode(".//div[contains(@class, 'lister-item-image')]").Descendants("img").First();
            return itemImageNode.Attributes["src"].Value;
        }
    }
}