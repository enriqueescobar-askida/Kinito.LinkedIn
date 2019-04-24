﻿/**
* MonsterOffer.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 24-04-2019
* OR 4/24/2019 10:52:10 AM
**/
namespace WpfApp.DataAccessLayer.Offers
{
    using HtmlAgilityPack;

    /// <summary>
    /// Defines the <see cref="MonsterOffer" />
    /// </summary>
    public class MonsterOffer : AbstractOffer
    {
        /// <summary>Initializes a new instance of the <see cref="MonsterOffer"/> class.</summary>
        public MonsterOffer() : base()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MonsterOffer"/> class.</summary>
        /// <param name="bodyHtmlNode">The body HTML node.</param>
        public MonsterOffer(HtmlNode bodyHtmlNode) : base(bodyHtmlNode)
        {
        }
    }
}
