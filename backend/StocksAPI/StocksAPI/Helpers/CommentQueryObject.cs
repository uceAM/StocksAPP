﻿namespace StocksAPI.Helpers
{
    public class CommentQueryObject
    {
        public string Symbol { get; set; } = string.Empty;
        public bool SortOrder { get; set; } = false;
    }
}
