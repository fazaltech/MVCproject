namespace MVCproject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblcustomers",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        customer_id = c.Decimal(nullable: false, precision: 18, scale: 2),
                        customer_code = c.String(),
                        customer_name = c.String(),
                        contact = c.String(),
                        address = c.String(),
                        flag = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblinvoices",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        invoice_id = c.Decimal(nullable: false, precision: 18, scale: 2),
                        customer_id = c.Decimal(precision: 18, scale: 2),
                        payment_type = c.Decimal(precision: 18, scale: 2),
                        total_amount = c.Decimal(precision: 18, scale: 2),
                        amount_tendered = c.Decimal(precision: 18, scale: 2),
                        bank_account_name = c.String(),
                        bank_account_number = c.String(),
                        date_recorded = c.String(),
                        user_id = c.Decimal(precision: 18, scale: 2),
                        flag = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblproductcategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        category_id = c.String(),
                        category_name = c.String(),
                        flag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblproducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.String(),
                        product_code = c.String(),
                        product_name = c.String(),
                        unit_id = c.String(),
                        category_id = c.String(),
                        unit_in_stock = c.Decimal(precision: 18, scale: 2),
                        unit_price = c.Decimal(precision: 18, scale: 2),
                        discount_percentage = c.Decimal(precision: 18, scale: 2),
                        recorder_level = c.Decimal(precision: 18, scale: 2),
                        user_id = c.String(),
                        flag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblproductunits",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        unit_id = c.String(),
                        unit_name = c.String(),
                        flag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblpurchaseorders",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        purchase_order_id = c.Decimal(precision: 18, scale: 2),
                        product_id = c.Decimal(precision: 18, scale: 2),
                        quantity = c.Decimal(precision: 18, scale: 2),
                        unit_price = c.Decimal(precision: 18, scale: 2),
                        sub_total = c.Decimal(precision: 18, scale: 2),
                        supplier_id = c.Decimal(precision: 18, scale: 2),
                        order_date = c.String(),
                        user_id = c.Decimal(precision: 18, scale: 2),
                        order_status = c.Int(),
                        flag = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblreceiveproducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        receive_product_id = c.String(),
                        product_id = c.String(),
                        quantity = c.Decimal(precision: 18, scale: 2),
                        unit_price = c.Decimal(precision: 18, scale: 2),
                        sub_total = c.Decimal(precision: 18, scale: 2),
                        supplier_id = c.Decimal(precision: 18, scale: 2),
                        received_date = c.Decimal(precision: 18, scale: 2),
                        user_id = c.Decimal(precision: 18, scale: 2),
                        flag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblsales",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        sale_id = c.Decimal(precision: 18, scale: 2),
                        invoice_id = c.Decimal(precision: 18, scale: 2),
                        product_id = c.Decimal(precision: 18, scale: 2),
                        quantity = c.Decimal(precision: 18, scale: 2),
                        unit_price = c.Decimal(precision: 18, scale: 2),
                        sub_total = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblsuppliers",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        supplier_int = c.Decimal(precision: 18, scale: 2),
                        supplier_code = c.String(),
                        supplier_name = c.String(),
                        supplier_contact = c.String(),
                        supplier_address = c.String(),
                        supplier_email = c.Binary(),
                        flag = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tblusers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Decimal(precision: 18, scale: 2),
                        user_name = c.String(),
                        password = c.String(),
                        fullname = c.String(),
                        designation = c.String(),
                        role = c.String(),
                        account_type = c.Int(),
                        flag = c.String(),
                        email_id = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblusers");
            DropTable("dbo.tblsuppliers");
            DropTable("dbo.tblsales");
            DropTable("dbo.tblreceiveproducts");
            DropTable("dbo.tblpurchaseorders");
            DropTable("dbo.tblproductunits");
            DropTable("dbo.tblproducts");
            DropTable("dbo.tblproductcategories");
            DropTable("dbo.tblinvoices");
            DropTable("dbo.tblcustomers");
        }
    }
}
