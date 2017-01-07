using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ArtWebApp.DataModels;
using System.Collections;
namespace ArtWebApp.DBTransaction
{
    public static class LaysheetTransaction
    {


       /// <summary>
       /// rolls delivered against a cutorder
       /// 
       /// </summary>
       /// <param name="asn"></param>
       /// <param name="atcid"></param>
       /// <returns></returns>
        public static DataTable getRolldeliveredagainstACutorder(int cutid)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, FabricRollmaster.SShade, FabricRollmaster.AShade, 
                         FabricRollmaster.ShadeGroup, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.WidthGroup, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.ShrinkageGroup, 
                         FabricRollmaster.SYard, FabricRollmaster.AYard, DORollDetails.CutID
FROM            DORollDetails INNER JOIN
                         FabricRollmaster ON DORollDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (DORollDetails.CutID = @cutid)";
                cmd.Parameters.AddWithValue("@cutid", cutid);
               
                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }

    }
}