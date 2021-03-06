﻿/**
* AbstractOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 17-04-2019
* OR 4/17/2019 3:30:07 PM
**/
namespace WpfApp.DataAccessLayer
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the <see cref="AbstractOffer" />
    /// </summary>
    public class AbstractOffer
    {
        #region AbstractPorperties
        /// <summary>Gets the meta title.</summary>
        /// <value>The meta title.</value>
        public string MetaTitle { get; internal set; }

        /// <summary>Gets the meta title identifier.</summary>
        /// <value>The meta title identifier.</value>
        public string MetaTitleId { get; internal set; }

        /// <summary>Gets the meta company.</summary>
        /// <value>The meta company.</value>
        public string MetaCompany { get; internal set; }

        /// <summary>Gets the meta company identifier.</summary>
        /// <value>The meta company identifier.</value>
        public string MetaCompanyId { get; internal set; }

        /// <summary>Gets the meta location.</summary>
        /// <value>The meta location.</value>
        public string MetaLocation { get; internal set; }

        /// <summary>Gets the meta date.</summary>
        /// <value>The meta date.</value>
        public DateTime MetaDate { get; internal set; }

        /// <summary>Gets the meta culture information.</summary>
        /// <value>The culture information.</value>
        public CultureInfo MetaCultureInfo { get; internal set; }

        /// <summary>Gets the meta URI.</summary>
        /// <value>The meta URI.</value>
        public Uri MetaUri { get; internal set; }

        /// <summary>Gets the meta source.</summary>
        /// <value>The meta source.</value>
        public string MetaSource { get; internal set; }

        /// <summary>Gets the meta map.</summary>
        /// <value>The meta map.</value>
        public string MetaMap { get; internal set; }
        #endregion

        #region AbstractConstructor
        /// <summary>Initializes a new instance of the <see cref="AbstractOffer"/> class.</summary>
        public AbstractOffer() : this(null, String.Empty, null)
        {
            this.MetaCultureInfo = CultureInfo.InvariantCulture;
            this.MetaUri = null;
            //this.MetaDate = this.GetMetaDate(null);
            this.MetaSource = String.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="AbstractOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        /// <param name="lang"></param>
        public AbstractOffer(HtmlNode bodyHtmlNode, string lang, Uri uri)
        {
            this.MetaCultureInfo = (!String.IsNullOrWhiteSpace(lang))
                ? new CultureInfo(lang)
                : CultureInfo.InvariantCulture;
            this.MetaUri = this.GetMetaUri(uri);
            //this.MetaDate = this.GetMetaDate(bodyHtmlNode);
            this.MetaSource = (uri == null) ? null : uri.AbsoluteUri;
        }
        #endregion

        #region PublicMethods
        /// <summary>Converts to JSON.</summary>
        /// <returns>JSON representation string</returns>
        public string ToJson() => JsonConvert.SerializeObject(this);
        #endregion

        #region PublicOverrideMethods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString() => "AbstractOffer";

        /// <summary>Gets the inner text from a class in body HTML node.</summary>
        /// <param name="aClass">a class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromAClassInBodyHtmlNode(string aClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//a[@class ='" + aClass + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from div class in body HTML node.</summary>
        /// <param name="divClass">The div class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromDivClassInBodyHtmlNode(string divClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//div[@class ='" + divClass + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the HTML node from div class in body HTML node.</summary>
        /// <param name="divClass">The div class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public HtmlNode GetHtmlNodeFromDivClassInBodyHtmlNode(string divClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//div[@class ='" + divClass + "']");

        /// <summary>Gets the ul HTML node from div class in body HTML node.</summary>
        /// <param name="divClass">The div class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public HtmlNode GetUlHtmlNodeNodeFromDivClassInBodyHtmlNode(string divClass, HtmlNode bodyHtmlNode)
            => this.GetHtmlNodeFromDivClassInBodyHtmlNode(divClass, bodyHtmlNode).ChildNodes[1];

        /// <summary>Gets the HTML node collection from dd class in body HTML node.</summary>
        /// <param name="ddClass">The dd class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public HtmlNodeCollection GetHtmlNodeCollectionFromDdClassInBodyHtmlNode(string ddClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectNodes("//dd[@class ='" + ddClass + "']");

        /// <summary>Gets the HTML node collection from div class in body HTML node.</summary>
        /// <param name="divClass">The div class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public HtmlNodeCollection GetHtmlNodeCollectionFromDivClassInBodyHtmlNode(string divClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectNodes("//div[@class ='" + divClass + "']");

        /// <summary>Gets the inner text from div identifier.</summary>
        /// <param name="divId">The div identifier.</param>
        /// <param name="bodyHtmlNode"></param>
        public string GetInnerTextFromDivIdInBodyHtmlNode(string divId, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//div[@id ='" + divId + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the HTML node from div identifier in body HTML node.</summary>
        /// <param name="divId">The div identifier.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public HtmlNode GetHtmlNodeFromDivIdInBodyHtmlNode(string divId, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//div[@id ='" + divId + "']");

        /// <summary>Gets the inner text from class identifier.</summary>
        /// <param name="h1Class">The identifier H1 class.</param>
        /// <param name="bodyHtmlNode"></param>
        public string GetInnerTextFromH1ClassInBodyHtmlNode(string h1Class, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//h1[@class ='" + h1Class + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from h1 identifier in body HTML node.</summary>
        /// <param name="h1Id">The h1 identifier.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromH1IdInBodyHtmlNode(string h1Id, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//h1[@id ='" + h1Id + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from class identifier.</summary>
        /// <param name="h2Class">The identifier H2 class.</param>
        /// <param name="bodyHtmlNode"></param>
        public string GetInnerTextFromH2ClassInBodyHtmlNode(string h2Class, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//h2[@class ='" + h2Class + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from h3 class in body HTML node.</summary>
        /// <param name="h3Class">The h3 class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromH3ClassInBodyHtmlNode(string h3Class, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//h3[@class ='" + h3Class + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from li class in body HTML node.</summary>
        /// <param name="liClass">The li class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromLiClassInBodyHtmlNode(string liClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//li[@class ='" + liClass + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from td class in body HTML node.</summary>
        /// <param name="tdClass">The td class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromTdClassInBodyHtmlNode(string tdClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//td[@class ='" + tdClass + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from span class in body HTML node.</summary>
        /// <param name="spanClass">The span class.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromSpanClassInBodyHtmlNode(string spanClass, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//span[@class ='" + spanClass + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from span data name in body HTML node.</summary>
        /// <param name="spanDataName">Name of the span data.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromSpanDataNameInBodyHtmlNode(string spanDataName, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//span[@data-name ='" + spanDataName + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from span identifier in body HTML node.</summary>
        /// <param name="spanId">The span identifier.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromSpanIdInBodyHtmlNode(string spanId, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//span[@id ='" + spanId + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Gets the inner text from span item property in body HTML node.</summary>
        /// <param name="spanItemProp">The span item property.</param>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public string GetInnerTextFromSpanItemPropInBodyHtmlNode(string spanItemProp, HtmlNode bodyHtmlNode)
            => bodyHtmlNode.SelectSingleNode("//span[@itemprop ='" + spanItemProp + "']").InnerText.TrimStart().TrimEnd().Trim();

        /// <summary>Chomps the specified string to chomp.</summary>
        /// <param name="stringToChomp">The string to chomp.</param>
        /// <returns>String chomp</returns>
        public string Chomp(string stringToChomp)
            => stringToChomp.TrimStart('\t').TrimEnd('\t').Trim('\t')
                .TrimStart('\n').TrimEnd('\n').Trim('\n')
                .TrimStart().TrimEnd().Trim();
        #endregion

        #region ProtectedVirtualMethods
        /// <summary>Gets the meta title.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual string GetMetaTitle(HtmlNode bodyHtmlNode) => @"Title";

        /// <summary>Gets the meta company.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual string GetMetaCompany(HtmlNode bodyHtmlNode) => @"Company";

        /// <summary>Gets the meta location.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual string GetMetaLocation(HtmlNode bodyHtmlNode) => @"City";

        /// <summary>Gets the meta date.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual DateTime GetMetaDate(HtmlNode bodyHtmlNode)
            => Convert.ToDateTime(new DateTime(2000, 01, 01), this.MetaCultureInfo);

        /// <summary>Gets the meta URI.</summary>
        /// <param name="uri">The URI.</param>
        public virtual Uri GetMetaUri(Uri uri) => (uri == null) ? null : uri;

        /// <summary>Gets the meta source.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual string GetMetaSource(HtmlNode bodyHtmlNode) => @"https://www.google.com/search?q=";

        /// <summary>Gets the meta map.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public virtual string GetMetaMap(HtmlNode bodyHtmlNode) => @"https://www.google.com/maps/search/";
        #endregion

        #region PrivateMethods
        #endregion
    }
}
