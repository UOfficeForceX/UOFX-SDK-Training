# U-Office Force X OpenAPI SDK 範例程式

U-Office Force X 提供 OpenAPI，讓外部系統（如 ERP、HR、CIM 等）可以透過 API 與 U-Office Force X 進行互動。<br>
若直接呼叫 OpenAPI，需自行組合 Request、URL 與 Header；使用 OpenAPI SDK 則可透過 .NET 方法呼叫 API，並由 SDK 處理呼叫與回應。

若想了解 OpenAPI SDK 的詳細使用方式，說明文件如下：
* [OpenAPI 使用指南](https://docs.uof.tw/techdoc/open-api/getting-started/overview/)
* [OpenAPI SDK 說明與使用方式](https://docs.uof.tw/techdoc/open-api/sdk/what-is-sdk/)
* [OpenAPI SDK 類別庫文件](https://docs.uof.tw/openapi/sdk/api/Ede.Uofx.OpenApi.Sdk.NetStd.html)
* [OpenAPI API 文件](https://docs.uof.tw/openapi/index.html)

為了更好理解如何應用到實際場景，我們提供了以下 OpenAPI SDK 的應用情境說明：
* [【表單管理 + URL 登入驗證】應用情境 - ERP 客製 UOFX 簽核箱](https://docs.uof.tw/techdoc/use-case/approval-box/)
* [【外部起單】應用情境 - ERP 採購單自動觸發 BPM 流程](https://docs.uof.tw/techdoc/use-case/purchase-order/)
* [【維護組織人員】應用情境 - 組織人員同步](https://docs.uof.tw/techdoc/use-case/org-sync/)
* [【自訂登入驗證】應用情境 - 使用 Google 帳號登入 UOFX](https://docs.uof.tw/techdoc/use-case/auth-thirdparty/)
* [【取得出勤刷卡】應用情境 - 打卡資料與 HR 系統整合](https://docs.uof.tw/techdoc/open-api/sdk/sample/punch/first-sample/)
