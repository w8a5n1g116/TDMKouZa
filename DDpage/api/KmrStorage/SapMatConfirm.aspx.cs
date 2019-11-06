using System;
using System.Net;
using DDpage.KmrAppMatChWebReference;


namespace DDpage.api.KmrStorage
{
    public partial class SapMatConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ZSPP_MAREQ_HEAD sap_head = new ZSPP_MAREQ_HEAD();
            sap_head.AUFNR = "3100001070";
            sap_head.GSBER = "0023";
            sap_head.SGTXT = "0010";


            ZSPP_MAREQ_ITEM[] sap_item = new ZSPP_MAREQ_ITEM[2];
            ZSPP_MAREQ_MSG[] sap_msg = new ZSPP_MAREQ_MSG[1];
            BAPIRET2[] sap_return = new BAPIRET2[1];

            ZSPP_MAREQ_ITEM sap_lineItem1 = new ZSPP_MAREQ_ITEM();
            ZSPP_MAREQ_ITEM sap_lineItem2 = new ZSPP_MAREQ_ITEM();

            sap_lineItem1.MATNR = "1005605";
            //sap_lineItem.WERKS = "1100";
            //sap_lineItem.CHARG = "000000213";
            sap_lineItem1.LGORT = "1001";
            sap_lineItem1.MENGE = 3;
            sap_lineItem1.MENGESpecified = true;
            //sap_lineItem.AUFNR = "3100001031";
            //sap_lineItem.GSBER = "0001";

            sap_item[0] = sap_lineItem1;


            sap_lineItem2.MATNR = "1005608";
            //sap_lineItem.WERKS = "1100";
            //sap_lineItem.CHARG = "000000213";
            sap_lineItem2.LGORT = "1001";
            sap_lineItem2.MENGE = 10;
            sap_lineItem2.MENGESpecified = true;

            sap_item[1] = sap_lineItem2;


            SI_ZPP_MATERIAL_REQService sap_serv = new SI_ZPP_MATERIAL_REQService();
            sap_serv.Credentials = new NetworkCredential("helb","init1234");

            string e_msgty= "";

            //string userName = "helb";
            //string password = "init1234";

            string rt_msg = sap_serv.SI_ZPP_MATERIAL_REQ(sap_head,ref sap_msg,ref sap_return,ref sap_item,out e_msgty);
            //接口返回详细信息处理
            foreach (BAPIRET2 rt_return in sap_return) {
                rt_return.TYPE = "E";
            }
            //接口返回详细信息处理
            Response.Write(rt_msg);

            ////sap_request.IS_HEAD = sap_head;
            ////sap_request.IT_ITEM = sap_item;

           

            //string E_MSGTY = "";

            //string endPoint = "HTTP_Port";

            //SI_ZPP_MATERIAL_REQClient sub1 = new SI_ZPP_MATERIAL_REQClient(endPoint);



            //NetworkCredential networkCredential = new NetworkCredential();

            ////sub1.ClientCredentials = new NetworkCredential("helb", "init1234");



            //string res_msg = sub1.SI_ZPP_MATERIAL_REQ(sap_head, ref sap_msg, ref sap_response.ET_RETURN,ref sap_item, out E_MSGTY);

            //Response.Write(res_msg);


            //request.userid = arr1[0];
            //request.username = arr1[1];

            //submitPlanApproveResponse response = new submitPlanApproveResponse();

            //finishServiceClient ss = new finishServiceClient();
            //response = ss.submitPlanApprove(request);
            //string re = "";
            //re = "success";
            //Response.Write(re);

        }
    }
}