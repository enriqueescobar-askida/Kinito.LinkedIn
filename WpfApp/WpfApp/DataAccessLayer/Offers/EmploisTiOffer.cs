﻿/**
* EmploisTiOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 24-04-2019
* OR 4/24/2019 10:52:10 AM
**/
namespace WpfApp.DataAccessLayer.Offers
{
    using HtmlAgilityPack;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="EmploisTiOffer" />
    /// </summary>
    public class EmploisTiOffer : AbstractOffer, IParseable
    {
        /// <summary>Initializes a new instance of the <see cref="EmploisTiOffer"/> class.</summary>
        public EmploisTiOffer() : this(null, String.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EmploisTiOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        /// <param name="lang"></param>
        /// <param name="uri"></param>
        public EmploisTiOffer(HtmlNode bodyHtmlNode, string lang, Uri uri)
        {
            this.MetaCultureInfo = (!String.IsNullOrWhiteSpace(lang))
                ? new CultureInfo(lang)
                : CultureInfo.InvariantCulture;
            this.MetaTitle = this.GetMetaTitle(bodyHtmlNode);
            this.MetaTitleId = this.GetMetaTitleId(uri);
            this.MetaCompany = this.GetMetaCompany(bodyHtmlNode);
            this.MetaCompanyId = this.GetMetaCompanyId(uri);
            this.MetaLocation = this.Chomp(this.GetMetaLocation(bodyHtmlNode));
            this.MetaDate = Convert.ToDateTime(this.GetMetaDate(bodyHtmlNode), this.MetaCultureInfo);
            this.MetaUri = this.GetMetaUri(uri);
            this.MetaSource = this.GetMetaSource(bodyHtmlNode);
            this.MetaMap = this.GetMetaMap(bodyHtmlNode);
        }

        #region PublicSealedOverrideMethods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public sealed override string ToString() => "EmploisTI";
        #endregion

        #region InterfaceMethods
        /// <summary>Gets the meta title identifier.</summary>
        /// <param name="uri">The URI.</param>
        public string GetMetaTitleId(Uri uri) => base.MetaTitleId;

        /// <summary>Gets the meta company identifier.</summary>
        /// <param name="uri">The URI.</param>
        public string GetMetaCompanyId(Uri uri) => base.MetaCompanyId;
        #endregion

        #region ProtectedSealedOverrideMethods
        /// <summary>Gets the meta tile.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaTitle(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH1ClassInBodyHtmlNode("page-title", bodyHtmlNode);

        /// <summary>Gets the meta company.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaCompany(HtmlNode bodyHtmlNode)
            => this.GetHtmlNodeFromDivIdInBodyHtmlNode("ajax-box-content", bodyHtmlNode)
                .SelectSingleNode("//article").ChildNodes.LastOrDefault().ChildNodes[3].ChildNodes[1]
                .InnerHtml.TrimStart().TrimEnd().Trim()
                .Replace("\t", "").Replace(" \n", "").Replace("\n", "")
                .Split(new[] { "span" }, StringSplitOptions.RemoveEmptyEntries)[1]
                .Replace("<", ">").Replace("/", ">").Replace(">", "");

        /// <summary>Gets the meta location.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaLocation(HtmlNode bodyHtmlNode)
            => this.GetHtmlNodeFromDivIdInBodyHtmlNode("ajax-box-content", bodyHtmlNode)
                .SelectSingleNode("//article").ChildNodes.LastOrDefault().ChildNodes[3].ChildNodes[3]
                .InnerHtml.TrimStart().TrimEnd().Trim()
                .Replace("\t", "").Replace(" \n", "").Replace("\n", "")
                .Split(new[] { "span" }, StringSplitOptions.RemoveEmptyEntries)[1]
                .Replace("<", ">").Replace("/", ">").Replace(">", "");

        /// <summary>Gets the meta date.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override DateTime GetMetaDate(HtmlNode bodyHtmlNode)
            => Convert.ToDateTime(DateTime.Today, this.MetaCultureInfo);

        /// <summary>Gets the meta URI.</summary>
        /// <param name="uri">The URI.</param>
        public sealed override Uri GetMetaUri(Uri uri)
            => (uri?.AbsoluteUri.Contains("?") == true)
                ? new Uri(uri.AbsoluteUri.Split(new[] { "?utm_medium" }, StringSplitOptions.None)[0])
                : uri;

        /// <summary>Gets the meta source.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaSource(HtmlNode bodyHtmlNode)
            => base.GetMetaSource(bodyHtmlNode) + this.MetaCompany.Replace(" ", "+") + "+" +
               this.MetaLocation.Replace(" ", "+");

        /// <summary>Gets the meta map.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaMap(HtmlNode bodyHtmlNode)
            => base.GetMetaMap(bodyHtmlNode) + this.MetaCompany.Replace(" ", "+") + "+" +
               this.MetaLocation.Replace(" ", "+");
        #endregion

        #region PrivateMethods
        #endregion
    }
}
