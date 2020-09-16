using cn.bmob.api;
using cn.bmob.io;
using cn.bmob.json;
using cn.bmob.tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2020/04/16/21:20:05
//远程协助管理平台v0.4.0.28b
//.cs
//杨智杰
//云笔

public class TETTT : MonoBehaviour
{
    private static BmobUnity Bmob;
    private void Start()
    {
        BmobDebug.Register(print);
        
        BmobDebug.level = BmobDebug.Level.TRACE;
        Bmob = gameObject.GetComponent<BmobUnity>();
       
    }

    //对应要操作的数据表
    public const String TABLENAME = "Test";

    void CreateData()
    {
        var data = new BmobGameObject();

        System.Random rnd = new System.Random();
        //data.id = rnd.Next(-50, 170);
        data.Test = rnd.Next(-50, 170) + "123";

        Bmob.Create(TABLENAME, data,
                     (resp, exception) =>
                     {
                         if (exception != null)
                         {
                             print("保存失败, 失败原因为： " + exception.Message);
                             return;
                         }

                         print("保存成功, @" + resp.createdAt);
                     }
        );
    }

    void UpdateGame()
    {
        BmobGameObject game = new BmobGameObject();
        game.Test = "pn_123";


        Bmob.Update(TABLENAME, "d6b5319765", game, (resp, exception) =>
        {
            if (exception != null)
            {
                print("保存失败, 失败原因为： " + exception.Message);
                return;
            }

            print("保存成功, @" + resp.updatedAt);
        });
    }

    void DeleteGame()
    {
        Bmob.Delete(TABLENAME, "d6b5319765", (resp, exception) =>
        {
            if (exception != null)
            {
                print("删除失败, 失败原因为： " + exception.Message);
                return;
            }
            print("删除成功, @" + resp.msg);
        });
    }

    void FindQueryWithCount()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("Test", "140123");
        query.Count();
        Bmob.Find<BmobGameObject>(TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<BmobGameObject> list = resp.results;
            BmobInt count = resp.count;
            print("满足条件的对象个数为： " + count.Get());
            foreach (var game in list)
            {
                print("获取的对象为： " + toString(game));
            }
        });
    }

    void FindQuery()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("Test", "140123");
        Bmob.Find<BmobGameObject>(TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<BmobGameObject> list = resp.results;
            foreach (var game in list)
            {
                print("获取的对象为： " + toString(game));
            }
        });
    }
    void GetGame()
    {
        Bmob.Get<BmobGameObject>(TABLENAME, "ZtB1CCCS", (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            BmobGameObject game = resp;
            print("获取的对象为： " + toString(game));
        });
    }
    String toString(object obj)
    {
        //if (obj is IBmobWritable) {
        //		return JsonAdapter.JSON.ToJson ((IBmobWritable)obj); 
        //} else
        return JsonAdapter.JSON.ToDebugJsonString(obj);
    }
    void Signup()
    {
        MyBmobUser user = new MyBmobUser();
        user.username = "test";
        user.password = "123456";
        user.email = "support@bmob.cn";
        user.life = 0;
        user.attack = 0;

        Bmob.Signup<MyBmobUser>(user, (resp, exception) =>
        {
            if (exception != null)
            {
                print("注册失败, 失败原因为： " + exception.Message);
                return;
            }

            print("注册成功");
        });
    }

    void Login()
    {
        Bmob.Login<MyBmobUser>("test", "123456", (resp, exception) => {
            if (exception != null)
            {
                print("登录失败, 失败原因为： " + exception.Message);
                return;
            }

            print("登录成功, @" + resp.username + "(" + resp.life + ")$[" + resp.sessionToken + "]");

            print("登录成功, 当前用户对象Session： " + BmobUser.CurrentUser.sessionToken);
        });


    }
    void gotCurrentUser()
    {
        print("登录后用户： " + toString(BmobUser.CurrentUser));
        print("登录后用户： " + toString(BmobUser.CurrentUser.email));
    }
    void updateuser()
    {
        Bmob.Login<MyBmobUser>("test", "123456", (resp, ex) =>
        {
            print(resp.sessionToken);
            MyBmobUser u = new MyBmobUser();
            u.attack = 1000;
            Bmob.UpdateUser(resp.objectId, u, resp.sessionToken, (updateResp, updateException) =>
            {
                if (updateException != null)
                {
                    print("保存失败, 失败原因为： " + updateException.Message);
                    return;
                }

                print("保存成功, @" + updateResp.updatedAt);
            });
        });
    }


    void findAllUser()
    {

        BmobQuery query = new BmobQuery();
        //query.WhereEqualTo ("playerName", "123");
        query.Count();
        Bmob.Find<MyBmobUser>(MyBmobUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyBmobUser> list = resp.results;
            BmobInt count = resp.count;
            print("满足条件的对象个数为： " + count.Get());
            foreach (var game in list)
            {
                print("获取的对象为： " + toString(game));
            }
        });

    }

    void ResetPassword()
    {
        Bmob.Reset("1228267639@qq.com", (resp, exception) => {
            if (exception != null)
            {
                print("重置密码请求失败, 失败原因为： " + exception.Message);
                return;
            }

            print("重置密码请求发送成功！");
        });
    }

    void FileUpload()
    {
        Bmob.FileUpload("F:\\Star\\1024icon2.png", (resp, exception) => {
            if (exception != null)
            {
                print("上传请求失败, 失败原因为： " + exception.Message);
                return;
            }

            print("上传请求发送成功！" + toString(resp));
        });
    }

    protected void WWWFormRequest()
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("tab.text", new byte[] { 0, 1, 2 });

        if (form != null && form.headers.Count > 0)
        {
            var headers = new Hashtable(); // add content-type
            IDictionaryEnumerator formHeadersIterator = form.headers.GetEnumerator();
            while (formHeadersIterator.MoveNext())
                headers.Add((String)formHeadersIterator.Key, formHeadersIterator.Value);
        }
    }
    void endpoint()
    {
        Bmob.Endpoint<Hashtable>("test", (resp, exception) => {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            print("返回对象为： " + resp);
        });
    }
    void FindUser()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("username", "test");
        Bmob.Find<MyBmobUser>(BmobUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyBmobUser> list = resp.results;
            foreach (var user in list)
            {
                print("获取的对象为： " + toString(user));
            }
        });
    }

    void OnGUI()
    {
        float scale = 2.0f;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            scale = Screen.width / 320;
        }

        float btnWidth = 100 * scale;
        float btnHeight = 25 * scale;
        float btnTop = 10 * scale;
        GUI.skin.button.fontSize = Convert.ToInt32(12 * scale);

        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "新建数据"))
        {
            CreateData();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "更新数据"))
        {
            UpdateGame();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "删除数据"))
        {
            DeleteGame();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "查询数据"))
        {
            FindQueryWithCount();
        }
       btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "查询全部"))
        {
            FindQuery();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "获取数据"))
        {
            GetGame();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "注册"))
        {
            Signup();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "登录"))
        {
            Login();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "获取当前用户"))
        {
            gotCurrentUser();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "更新用户数据"))
        {
            updateuser();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "查询用户"))
        {
            findAllUser();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "重置密码"))
        {
            ResetPassword();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "上传文件"))
        {
            FileUpload();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "查找用户"))
        {
            FindUser();
        }
        btnTop += btnHeight + 10 * scale;
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "endpoint"))
        {
            endpoint();
        }
        if (GUI.Button(new Rect((Screen.width - btnWidth) / 2 + btnWidth, btnTop, btnWidth, btnHeight), "查找用户"))
        {
            FindUser();
        }
    }
}



// 如果程序需要为用户添加额外的字段，需要继承BmobUser
public class MyBmobUser : BmobUser
{
    public BmobInt life { get; set; }

    public BmobInt attack { get; set; }

    public override void write(BmobOutput output, bool all)
    {
        base.write(output, all);

        output.Put("life", this.life);
        output.Put("attack", this.attack);
    }

    public override void readFields(BmobInput input)
    {
        base.readFields(input);

        this.life = input.getInt("life");
        this.attack = input.getInt("attack");
    }
}

class BmobGameObject : BmobTable
{

    private String fTable;
    //以下对应云端字段名称
    public BmobInt id { get; set; }
    public String Test { get; set; }


    //构造函数
    public BmobGameObject() { }

    //构造函数
    public BmobGameObject(String tableName)
    {
        this.fTable = tableName;
    }

    public override string table
    {
        get
        {
            if (fTable != null)
            {
                return fTable;
            }
            return base.table;
        }
    }

    //读字段信息
    public override void readFields(BmobInput input)
    {
        base.readFields(input);

        this.id = input.getInt("id");
        this.Test = input.getString("Test");
    }

    //写字段信息
    public override void write(BmobOutput output, bool all)
    {
        base.write(output, all);

        output.Put("id", this.id);
        output.Put("Test", this.Test);
    }
}