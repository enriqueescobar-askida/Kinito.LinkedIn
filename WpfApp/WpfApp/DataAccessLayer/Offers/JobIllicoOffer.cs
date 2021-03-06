﻿/**
* JobillicoOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 17-04-2019
* OR 4/17/2019 8:19:27 PM
**/
namespace WpfApp.DataAccessLayer.Offers
{
    using HtmlAgilityPack;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="JobIllicoOffer" />
    /// </summary>
    public class JobIllicoOffer : AbstractOffer, IParseable
    {
        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="JobIllicoOffer"/> class.</summary>
        public JobIllicoOffer() : this(null, String.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="JobIllicoOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The bodyHtmlNode<see cref="HtmlNode"/></param>
        /// <param name="lang"></param>
        /// <param name="uri"></param>
        public JobIllicoOffer(HtmlNode bodyHtmlNode, string lang, Uri uri)
        {
            this.MetaCultureInfo = (!String.IsNullOrWhiteSpace(lang))
                ? new CultureInfo(lang)
                : CultureInfo.InvariantCulture;
            bool isExpired =
                    bodyHtmlNode.InnerText.IndexOf("expired", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    bodyHtmlNode.InnerText.IndexOf("deactivated", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    bodyHtmlNode.InnerText.IndexOf("expirée", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    bodyHtmlNode.InnerText.IndexOf("désactivée", StringComparison.InvariantCultureIgnoreCase) >= 0;
            this.MetaTitle = isExpired ? base.GetMetaTitle(bodyHtmlNode) : this.GetMetaTitle(bodyHtmlNode);
            this.MetaTitleId = isExpired ? base.MetaTitleId : this.GetMetaTitleId(uri);
            this.MetaCompany = isExpired ? base.GetMetaCompany(bodyHtmlNode) : this.GetMetaCompany(bodyHtmlNode);
            this.MetaCompanyId = this.GetMetaCompanyId(uri);
            this.MetaLocation = isExpired ? base.GetMetaLocation(bodyHtmlNode) : this.GetMetaLocation(bodyHtmlNode);
            this.MetaDate =
                Convert.ToDateTime(isExpired ? base.GetMetaDate(bodyHtmlNode) : this.GetMetaDate(bodyHtmlNode), this.MetaCultureInfo);
            this.MetaUri = isExpired ? base.GetMetaUri(uri) : this.GetMetaUri(uri);
            this.MetaSource = isExpired ? this.MetaUri.AbsoluteUri : this.GetMetaSource(bodyHtmlNode);
            this.MetaMap = isExpired ? base.GetMetaMap(bodyHtmlNode) : this.GetMetaMap(bodyHtmlNode);
        }
        #endregion

        #region PublicSealedOverrideMethods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public sealed override string ToString() => "JobIllico";
        #endregion

        #region InterfaceMethods
        /// <summary>Gets the meta title identifier.</summary>
        /// <param name="uri">The URI.</param>
        public string GetMetaTitleId(Uri uri)
            => this.GetMetaUri(uri).AbsolutePath
                .Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

        /// <summary>Gets the meta company identifier.</summary>
        /// <param name="uri">The URI.</param>
        public string GetMetaCompanyId(Uri uri) => base.MetaCompanyId;
        #endregion

        #region ProtectedSealedOverrideMethods
        /// <summary>Gets the meta tile.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaTitle(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH1ClassInBodyHtmlNode("h1-class art-head", bodyHtmlNode);

        /// <summary>Gets the meta company.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaCompany(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH2ClassInBodyHtmlNode("companyName", bodyHtmlNode);

        /// <summary>Gets the meta location.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaLocation(HtmlNode bodyHtmlNode)
            => this.GetHtmlNodeFromDivClassInBodyHtmlNode("main-article-content col-md-8", bodyHtmlNode)
                .SelectSingleNode("//p//a//span").InnerText.Trim().TrimStart().TrimEnd()
                .Replace(Environment.NewLine, "").Replace("    ","");

        /// <summary>Gets the meta date.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override DateTime GetMetaDate(HtmlNode bodyHtmlNode)
            => DateTime.Today;

        /// <summary>Gets the meta URI.</summary>
        /// <param name="uri">The URI.</param>
        public sealed override Uri GetMetaUri(Uri uri)
            => (uri?.AbsoluteUri.Contains("?") == true)
                ? new Uri(uri.AbsoluteUri.Split(new[] { "?utm_source" }, StringSplitOptions.None)[0])
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
