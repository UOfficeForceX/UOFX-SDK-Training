using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SDK_FirstSample.Models;

namespace SDK_FirstSample.Service
{
    public class ERPService
    {
        private readonly string _connectionString;

        public ERPService(IConfiguration configuration)
        {
            // 從 appsettings.json 中讀取 ConnectionString
            _connectionString = configuration.GetConnectionString("Default");
        }

        /**
        * 新增一筆採購單及多筆採購明細
        * @param model 採購單資料
        * @return 新增的採購單編號
        */
        internal decimal InsertPurchase([Bind] PurchaseModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlPurchase = @"
                        INSERT INTO Purchases
                        (SupplierID, Status, PurchaseDate, PurchaseType)
                        VALUES
                        (@SupplierID, @Status, @PurchaseDate, @PurchaseType)
                        SELECT SCOPE_IDENTITY(); -- 取得自動生成的 PurchaseID
                    ";

                using (var command = new SqlCommand(sqlPurchase, connection))
                {
                    // 置換參數
                    command.Parameters.AddWithValue("@SupplierID", model.SupplierID);
                    command.Parameters.AddWithValue("@Status", model.Status);
                    command.Parameters.AddWithValue("@PurchaseDate", model.PurchaseDate);
                    command.Parameters.AddWithValue("@PurchaseType", model.PurchaseType);

                    // 取得自動生成的 PurchaseID
                    model.PurchaseID = Convert.ToInt32(command.ExecuteScalar());
                }

                foreach (var purchaseDetail in model.PurchaseDetails)
                {
                    string sqlDetail = @"
                        INSERT INTO [Purchase Details]
                        (PurchaseID, ProductID, Quantity, UnitPrice)
                        VALUES
                        (@PurchaseID, @ProductID, @Quantity, @UnitPrice)
                    ";

                    using (var commandDetail = new SqlCommand(sqlDetail, connection))
                    {
                        // 置換參數
                        commandDetail.Parameters.AddWithValue("@PurchaseID", model.PurchaseID);
                        commandDetail.Parameters.AddWithValue("@ProductID", purchaseDetail.ProductID);
                        commandDetail.Parameters.AddWithValue("@Quantity", purchaseDetail.Quantity);
                        commandDetail.Parameters.AddWithValue("@UnitPrice", purchaseDetail.UnitPrice);

                        commandDetail.ExecuteNonQuery();
                    }
                }
                return model.PurchaseID;
            }
        }

        /**
        * 更新採購單狀態
        * @param model 更新狀態資料
        * @return 更新後的採購單狀態
        */
        internal string UpdatePurchaseStatus([Bind] UpdatePurchaseStatusModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlUpdateStatus = @"
                        UPDATE Purchases
                        SET Status = @Status
                        WHERE PurchaseID = @PurchaseID
                    ";

                using (var command = new SqlCommand(sqlUpdateStatus, connection))
                {
                    // 置換參數
                    command.Parameters.AddWithValue("@PurchaseID", model.PurchaseID);

                    // 轉換狀態文字
                    string status = "";
                    switch (model.Status)
                    {
                        case "Approve":
                            status = "已通過";
                            break;
                        case "Reject":
                            status = "已否決";
                            break;
                        case "Cancel":
                            status = "已作廢";
                            break;
                        default:
                            status = model.Status;
                            break;
                    }
                    command.Parameters.AddWithValue("@Status", status);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            return model.Status;
        }

        /**
        * 更新簽核資料
        * @param model 更新簽核資料
        * @return 更新後的簽核ID
        */
        internal string UpdateBpm([Bind] UpdateBpmModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                        UPDATE Purchases
                        SET BpmID = @BpmID
                        WHERE PurchaseID = @PurchaseID
                    ";

                using (var command = new SqlCommand(sql, connection))
                {
                    // 置換參數
                    command.Parameters.AddWithValue("@BpmID", model.BpmID);
                    command.Parameters.AddWithValue("@PurchaseID", model.PurchaseID);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            return model.BpmID;
        }
    }
}
