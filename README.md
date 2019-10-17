# Excel-Exporting-Server-side
EJ2-DataGrid-Core-Server-Side-Excel-Export

In the toolbarClick handler, we have used form submit action to call the server side exporting method(**ExcelExport**) and send the Grid properties to the server side as JSON string. After receiving this properties in server side, you can
 Deserialized this records by using the model class (i.e **ExportModel**) used to deserialize this grid model. We can get the column details through this deserialized model, and you can use this to customize your export based on Grid at server side. And
 finally we have provided datasource to the exporting file through the __ImportData__ method.
 
 [comment]: <> (I251431 -Export the EJ2 Grid with codes from the server side)
