﻿/**
* EspressoJobsOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 24-04-2019
* OR 4/24/2019 10:52:10 AM
**/
namespace WpfApp.DataAccessLayer.Offers
{
    using HtmlAgilityPack;

    /// <summary>
    /// Defines the <see cref="EspressoJobsOffer" />
    /// </summary>
    public class EspressoJobsOffer : AbstractOffer
    {
        /// <summary>Initializes a new instance of the <see cref="EspressoJobsOffer"/> class.</summary>
        public EspressoJobsOffer() : base()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EspressoJobsOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public EspressoJobsOffer(HtmlNode bodyHtmlNode) : base(bodyHtmlNode)
        {
        }

        #region PublicSealedOverrideMethods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public sealed override string ToString() => "Espresso-Jobs";
        #endregion
    }
}
