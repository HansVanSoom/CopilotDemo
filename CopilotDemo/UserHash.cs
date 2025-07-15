using Azure;
using Azure.Data.Tables;
using System;

namespace CopilotDemo
{
    //public class UserHash : ITableEntity
    //{
    //    // public string PartitionKey { get => "users"; set { } }
    //    public string PartitionKey { get; set; } = "users";
    //    //public required string RowKey { get; set; }
    //    public string RowKey { get; set; } = default!;
    //    public DateTimeOffset? Timestamp { get; set; }
    //    public ETag ETag { get; set; } 
    //}

    public class UserHash : ITableEntity
    {
        public string PartitionKey { get; set; } = "users";
        public string RowKey { get; set; } = default!;
        public string HashValue { get; set; } = default!;
        public string Salt { get; set; } = default!; // or whatever second value is

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

}


