using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DocumentModel;

namespace DemoChess.GameScripts
{
    public class GameModel
    {
        public String GameId { get; set; }
        public int likes = 0;
        public long Time { get; set; }

        public List<RoundModel> Rounds = new List<RoundModel>();

        public List<Document> GetDocumentList()
        {
            List<Document> documents = new List<Document>();

            foreach (var round in Rounds)
            {
                Document document = new Document();

                document["boardState"] = round.BoardState;
                document["roundNumber"] = round.RoundNumber;
                document["votes"] = round.GetDocumentList();

                documents.Add(document);

            }


            return documents;
        }


 
    }
}