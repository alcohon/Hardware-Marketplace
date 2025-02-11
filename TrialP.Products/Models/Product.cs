﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrialP.Products.Models;

public class AggregatedReviews
{
    public int InternalRating { get; set; }
    public int InternalCount { get; set; }
    public int ExternalCount { get; set; }
    public int ExternalRating { get; set; }

    [JsonPropertyName("count")]
    public int TotalCount { get; set; }
    [JsonPropertyName("rating")]
    public int TotalRating { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
    public string? Url { get; set; }
}
public class Images
{
    public string Header { get; set; }
}

public class Offers
{
    public int Count { get; set; }
}
public class Price
{
    public string Amount { get; set; }
    public string Currency { get; set; }
}
public class Prices
{
    [JsonPropertyName("offers")]
    public Offers ApiOffers { get; set; }


    [JsonPropertyName("price_min")]
    public Price PriceMin { get; set; }


    [JsonPropertyName("price_max")]
    public Price PriceMax { get; set; }
}
public partial class Product
{
    private Prices prices;
    private Images images;


    [JsonPropertyName("dbId")]
    public Guid Id { get; set; }

    [JsonPropertyName("id")]
    public int? ApiId { get; set; }

    public string Key { get; set; }

    public string Name { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("name_prefix")]
    public string? NamePrefix { get; set; }

    [JsonPropertyName("extended_name")]
    public string ExtendedName { get; set; }

    public string? Status { get; set; }

    public string ImageHeader { get; set; }

    [NotMapped]
    public Images Images
    {
        get
        {
            Images newImages = new();
            newImages.Header = images?.Header ?? ImageHeader;
            return images ?? newImages;
        }
        set => images = value;
    }

    [NotMapped]
    [JsonPropertyName("category_name")]
    public string CategoryName { get => SubSubCategory?.ApiName ?? ""; }
    public string Description { get; set; }

    [JsonPropertyName("micro_description")]
    public string? Microdescription { get; set; }

    [JsonIgnore]
    public decimal? PriceMin { get; set; }

    [JsonIgnore]
    public decimal? PriceMax { get; set; }


    [NotMapped]
    public Prices Prices
    {
        get
        {
            Prices pricesNew = new();
            pricesNew.PriceMax = prices?.PriceMax ?? new Price() { Amount = PriceMax?.ToString() ?? "", Currency = "BYN" };
            pricesNew.PriceMin = prices?.PriceMin ?? new Price() { Amount = PriceMin?.ToString() ?? "", Currency = "BYN" };
            pricesNew.ApiOffers = prices?.ApiOffers ?? new Offers() { Count = Offers.Value };
            return prices ?? pricesNew;
        }
        set => prices = value;
    }

    [NotMapped]
    [JsonPropertyName("reviews")]
    public AggregatedReviews AggregatedReviews { get; set; }

    public int? Offers { get; set; }
    [JsonIgnore]
    public Guid? SubSubCategoryId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    [JsonIgnore]
    public virtual ICollection<PositionsPrimary> PositionsPrimaries { get; } = new List<PositionsPrimary>();
    [JsonIgnore]
    public virtual SubSubCategory SubSubCategory { get; set; }
}
