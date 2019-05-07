﻿/**
* IsartaOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 24-04-2019
* OR 4/24/2019 10:52:10 AM
**/
namespace WpfApp.DataAccessLayer.Offers
{
    using HtmlAgilityPack;

    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the <see cref="IsartaOffer" />
    /// </summary>
    public class IsartaOffer : AbstractOffer
    {
        /// <summary>Initializes a new instance of the <see cref="IsartaOffer"/> class.</summary>
        public IsartaOffer() : base()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="IsartaOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public IsartaOffer(HtmlNode bodyHtmlNode, string lang) : base(bodyHtmlNode)
        {
            this.MetaTitle = this.GetMetaTitle(bodyHtmlNode);
            this.MetaCompany = this.GetMetaCompany(bodyHtmlNode);
            this.MetaLocation = this.GetMetaLocation(bodyHtmlNode);
            this.MetaDate = Convert.ToDateTime(this.GetMetaDate(bodyHtmlNode), new CultureInfo(lang));
            this.MetaSource = this.GetMetaSource(bodyHtmlNode);
        }

        #region PublicSealedOverrideMethods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public sealed override string ToString() => "Isarta";
        #endregion

        #region ProtectedSealedOverrideMethods
        /// <summary>Gets the meta tile.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaTitle(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH2ClassInBodyHtmlNode("offreEmploi", bodyHtmlNode);

        /// <summary>Gets the meta company.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        /// <returns></returns>
        public sealed override string GetMetaCompany(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH1ClassInBodyHtmlNode("offrCmpgn", bodyHtmlNode);

        /// <summary>Gets the meta location.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaLocation(HtmlNode bodyHtmlNode)
            => this.GetInnerTextFromH3ClassInBodyHtmlNode("Lieu", bodyHtmlNode);

        /// <summary>Gets the meta date.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override DateTime GetMetaDate(HtmlNode bodyHtmlNode)
        {
            string s = this.GetInnerTextFromTdClassInBodyHtmlNode("table table-curved", bodyHtmlNode)
                        .Split(':')[1].Replace(" \t", "").Split('\t')[0].TrimStart().TrimEnd().Trim()
                        .Replace("&nbsp;", "");
            return base.GetMetaDate(bodyHtmlNode);
        }

        /// <summary>Gets the meta source.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public sealed override string GetMetaSource(HtmlNode bodyHtmlNode)
            => @"https://www.google.com/search?q=" + this.MetaCompany.Replace(" ", "+") + "+" +
               this.MetaLocation.Replace(" ", "+");
        #endregion

        #region PrivateMethods
        #endregion
    }
}
