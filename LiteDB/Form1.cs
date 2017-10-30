using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiteDB
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//增
        {
            var db = new LiteDatabase("sample.db");
            //获取 customers 集合，如果没有会创建，相当于表
            var col = db.GetCollection<Customer>("customers");
            //创建 customers 实例
            var customer = new Customer
            {
                Name = "John Doe",
                Phones = new string[] { "8000-0000", "9000-0000" },
                IsActive = true
            };
            // 将新的对象插入到数据表中，Id是自增，自动生成的
            col.Insert(customer);
            MessageBox.Show("完成");
        }

        private void button2_Click(object sender, EventArgs e)//删
        {
            var db = new LiteDatabase("sample.db");
            //获取 customers 集合，如果没有会创建，相当于表
            var col = db.GetCollection<Customer>("customers");
            col.Delete(Query.EQ("Name", "John Doe"));//id
        }

        private void button4_Click(object sender, EventArgs e)//改
        {
            var db = new LiteDatabase("sample.db");
            //获取 customers 集合，如果没有会创建，相当于表
            var col = db.GetCollection<Customer>("customers");
            var result = col.FindById(6);
            MessageBox.Show("完成");
            result.Name = "lh";
            col.Update(result);
        }

        private void button3_Click(object sender, EventArgs e)//查
        {
            var db = new LiteDatabase("sample.db");
            //获取 customers 集合，如果没有会创建，相当于表
            var col = db.GetCollection<Customer>("customers");
            var results = col.Find(Query.EQ("Name","John Doe"));
            //Query.All 返回所有的数据，可以使用指定的索引字段进行排序
            //Query.EQ 查找返回和指定字段值相等的数据
            //Query.LT / LTE 查找 < 或 <= 某个值的数据
            //Query.GT / GTE 查找 > 或 >= 某个值的数据
            //Query.Between 查找在指定区间范围内的数据
            //Query.In - 和SQL的in类似吧，查找和列表中值相等的数据
            //Query.Not - 和EQ相反，是不等于某个值的数据
            //Query.StartsWith 查找以某个字符串开头的数据
            //Query.Contains 查找保护某个字符串的数据，这个查询只扫描索引
            //Query.And 2个查询的交集
            //Query.Or 2个查询结果的并集

            //Linq查询
            //FindAll: 查找表或者集合中所有的结果记录
            //FindOne:返回第一个或者默认的结果
            //FindById: 通过索引返回单个结果
            //Find: 使用查询表达式或者linq表达式查询返回结果
            //var collection = db.GetCollection<Customer>("customer");
            //var results = collection.Find(x => x.Name == "John Doe");
            //var results = collection.Find(x => x.Age > 30);
            //var results = collection.Find(x => x.Name.StartsWith("John") && x.Age > 30);
            //查找满足关键词的题目
            //var models = list.Where(n => n.ProbText.Contains(txtKeyValue.Text)).ToList();
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }
    }
}
