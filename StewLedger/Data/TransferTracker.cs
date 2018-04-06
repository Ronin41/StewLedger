using StewLedger.Tables.Accounts;
using StewLedger.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StewLedger.Model;


namespace StewLedger.Data
{
   
    public class TransferList : ObservableCollection<TransferModel>
    {
        public TransferList()
        {
            //this.Add(transferStruct);
        }
    }

    public class TransferTracker
    {


        public TransferList LoadTransfers(bool autoOnly)
        {
            TransferList transferList = new TransferList();
           
            TransferTable transferTable = new TransferTable();
            List<string> list = transferTable.ReturnRows(User._username);
            

            int numRecords = list.Count / 11;

            int item = 0, item1 = 1, item2 = 2, item3 = 3, item4 = 4, item5 = 5, item6=6, item7=7, item8=8, item9=9;

            for(int i = 0; i < numRecords; i++)
            {
                TransferModel transferStruct = new TransferModel();

                transferStruct.Id = Convert.ToInt32(list[item]);
            
                transferStruct.Source = list[item1];
                transferStruct.Dest = list[item2];
                transferStruct.SourceAcctId = Convert.ToInt32(list[item3]);
                transferStruct.DestAcctId = Convert.ToInt32(list[item4]);
                transferStruct.SourceId = Convert.ToInt32(list[item5]);
                transferStruct.DestId = Convert.ToInt32(list[item6]);
                transferStruct.Auto = list[item7];
                transferStruct.Date = Convert.ToDateTime(list[item8]);
                transferStruct.AutoDate = Convert.ToDateTime(list[item9]);


                if (autoOnly == true && transferStruct.Auto == "True")
                {
                    transferList.Add(transferStruct);
                }
                else if (autoOnly == false)
                {
                    transferList.Add(transferStruct);
                }

                

                item += 11;
                item1 += 11;
                item2 += 11;
                item3 += 11;
                item4 += 11;
                item5 += 11;
                item6 += 11;
                item7 += 11;
                item8 += 11;
                item9 += 11;

            }


            return transferList;
        }
    }
}
