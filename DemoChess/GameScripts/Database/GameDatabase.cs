using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.WebSockets;
using Amazon;
using Amazon.CognitoIdentity.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;

namespace DemoChess.GameScripts
{
    public class GameDatabase
    {
//        private AmazonDynamoDBClient client;
        private string tableName = "GameTable";


        public GameDatabase()
        {
            
        }

        public void CreateTable()
        {

            AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
            // ddbConfig.ServiceURL = "http://localhost:8000";
            AmazonDynamoDBClient client;
          

            try { client = new AmazonDynamoDBClient(RegionEndpoint.APSoutheast2); }
            catch (Exception ex)
            {
                Debug.WriteLine("\n Error: failed to create a DynamoDB client; " + ex.Message);
                return ;
            }

            CreateTableRequest createRequest = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                   new AttributeDefinition()
                   {
                       AttributeName = "gameId",
                       AttributeType = ScalarAttributeType.S
                   }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                   new KeySchemaElement()
                   {
                       AttributeName = "gameId",
                       KeyType = KeyType.HASH
                   }
                }
            };

            createRequest.ProvisionedThroughput = new ProvisionedThroughput(1,1);
            CreateTableResponse response;
            try
            {
                response = client.CreateTable(createRequest);
                Debug.WriteLine("Create Table Success");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public Document GetGameEntry(String Id)
        {
            if (Id != null)
            {
                Table table = GetTableObject(tableName);
                if (table != null)
                {
                    Document document = table.GetItem(Id);
                    Debug.WriteLine(document.ToJsonPretty());
                    return document;
                }
            }
            return null;
        }


        public void InsertEntry(GameModel gameModel)
        {
            Table tabel = GetTableObject(tableName);
            if (tabel != null)
            {
                Document document = new Document();

                document["gameId"] = gameModel.GameId;
                document["time"] = gameModel.Time;
                document["likes"] = gameModel.likes;
                document["roundlist"] = gameModel.GetDocumentList();


                try
                {
                    tabel.PutItem(document);
                    Debug.WriteLine("\nInsert successfull");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nFailed to insert "+ex.Message);
                }

            }


        }
        public static Table GetTableObject(string tableName)
        {
            // First, set up a DynamoDB client for DynamoDB Local
            AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
           // ddbConfig.ServiceURL = "http://localhost:8000";
           ddbConfig.RegionEndpoint = RegionEndpoint.APSoutheast2;
            AmazonDynamoDBClient client;
            
            try { client = new AmazonDynamoDBClient(RegionEndpoint.APSoutheast2); }
            catch (Exception ex)
            {
                Debug.WriteLine("\n Error: failed to create a DynamoDB client; " + ex.Message);
                return (null);
            }

            // Now, create a Table object for the specified table
            Table table = null;
            try { table = Table.LoadTable(client, tableName); }
            catch (Exception ex)
            {
                Debug.WriteLine("\n Error: failed to load the 'Movies' table; " + ex.Message);
                return (null);
            }
            return (table);
        }

        public List<Document> GetTableInfo()
        {
            Table gameTable = GetTableObject(tableName);
            ScanFilter scanFilter = new ScanFilter();
            
            Search search =  gameTable.Scan(scanFilter);
            List<Document> documentList = new List<Document>();
            documentList = search.GetNextSet();
            //            do
            //            {
            //                documentList = search.GetNextSet();
            //                foreach (var document in documentList)
            //                    Debug.WriteLine(document.ToJsonPretty());
            //            } while (!search.IsDone);

            return documentList;

        }



    }
}