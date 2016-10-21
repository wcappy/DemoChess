using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DocumentModel;

namespace DemoChess.GameScripts
{
    public class RoundModel
    {
        public int RoundNumber { get; set; }
        public String BoardState { get; set; }
        public Dictionary<String, int> MoveCounter = new Dictionary<string, int>();

        public List<Document> GetDocumentList()
        {
            List<Document> documents = new List<Document>();

            foreach (var move in MoveCounter)
            {
                Document document = new Document();

                document["moveKey"] = move.Key;
                document["count"] = move.Value;

                documents.Add(document);

            }


            return documents;
        }
    }
}