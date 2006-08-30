using System;
using System.Collections.Generic;
using System.Text;
using AlbumArtTagger.Amazon;

namespace AlbumArtTagger
{

    class AmazonAlbumArtFinder
    {

        private static string _amazonKey = "09XAA8GD0PQVCC8KR5G2";

        public static bool FindArtOnAmazon(string currAlbum, string currArtist, ref System.Collections.ArrayList artURLs)
        {
            Amazon.AWSECommerceService currAmazonService = new AWSECommerceService();
            Amazon.ItemSearch currItemSearch = new ItemSearch();
            Amazon.ItemSearchRequest currItemSearchRequest = new ItemSearchRequest();
            Amazon.ItemSearchResponse currItemSearchResponse;

            currItemSearch.SubscriptionId = _amazonKey;
            //itemSearch.AssociateTag = this.AssociateTag;
            currItemSearchRequest.Keywords = currAlbum.Trim();
            currItemSearchRequest.Artist = currArtist;
            currItemSearchRequest.SearchIndex = "Music";
            currItemSearchRequest.ResponseGroup = new string[] { "Small", "Images" };
            currItemSearch.Request = new ItemSearchRequest[1] { currItemSearchRequest };

            //send the query
            try
            {
                currItemSearchResponse = currAmazonService.ItemSearch(currItemSearch);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            Amazon.Items[] currItemsResponse = currItemSearchResponse.Items;
            if (currItemsResponse == null)
            {
                return false;
            }

            if (currItemsResponse.Length > 0 &&
                currItemsResponse[0].Item != null &&
                currItemsResponse[0].Item.Length > 0)
            {
                foreach (Amazon.Item currItem in currItemsResponse[0].Item)
                {
                    if (currItem.LargeImage != null && currItem.LargeImage.URL != "")
                    {
                        artURLs.Add(currItem.LargeImage.URL);
                    }
                    else if (currItem.MediumImage != null && currItem.MediumImage.URL != "")
                    {
                        artURLs.Add(currItem.MediumImage.URL);
                    }
                    else if (currItem.SmallImage != null && currItem.SmallImage.URL != "")
                    {
                        artURLs.Add(currItem.SmallImage.URL);
                    }
                }
            }

            if (artURLs.Count > 0)
                return true;
            else return false;

        }
    }
}
