package com.Unity.Tools;

import android.content.ComponentName;
import android.content.Context;

import java.io.File;
import java.lang.reflect.Field;

import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.support.v4.content.FileProvider;
import android.widget.Toast;
import android.content.IntentFilter;


public class UTool
{
    private static UTool  _instance;
    public static UTool  instance()
    {
        if(null == _instance)
        {
            _instance = new UTool();
        }
        return _instance;
    }
    private Context context;

    public  void Init(Context context)
    {
        this.context = context;
    }

    public void Toast(String msg)
    {
        Toast toast=Toast.makeText(context,msg,Toast.LENGTH_SHORT);
        toast.show();
    }

    public  int GetStatusBarHeight()
    {
        Class<?> c = null;
        Object obj = null;
        Field field = null;
        int x = 0, statusBarHeight = 0;
        try {
            c = Class.forName("com.android.internal.R$dimen");
            obj = c.newInstance();
            field = c.getField("status_bar_height");
            x = Integer.parseInt(field.get(obj).toString());
            statusBarHeight =  context.getResources().getDimensionPixelSize(x);
        } catch (Exception e1) {
            e1.printStackTrace();
        }
        return statusBarHeight;
    }

    public  void  SendBroadcast()
    {
        try{
            Intent intent = new Intent();
            intent.setAction("callme");
            intent.setComponent(new ComponentName(context,"com.unity3d.player.UABroadcastReceiver"));
            context.sendBroadcast(intent);
            Toast(context.getPackageName()+"");
        }
        catch (Exception e){
            Toast(e.toString());
            e.printStackTrace();
        }
    }
    public  void installApk(String apkFullPath)
    {
        try
        {
            File file = new File(apkFullPath);
            if (null == file){
                return;
            }
            if (!file.exists()){
                return;
            }
            Intent intent = new Intent(Intent.ACTION_VIEW);
            Uri apkUri =null;
            if(Build.VERSION.SDK_INT>=24)
            {
                apkUri = FileProvider.getUriForFile(context, context.getPackageName()+".fileprovider", file);
                intent.setFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
                intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
                //intent.setDataAndType(apkUri, "application/vnd.android.package-archive");
            }else{
                apkUri = Uri.fromFile(file);
                intent.setFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
                intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
            }
            intent.setDataAndType(apkUri, "application/vnd.android.package-archive");
            Toast.makeText(context, apkUri.getPath(), Toast.LENGTH_LONG).show();
            context.startActivity(intent);
        }
        catch (Exception e)
        {
            Toast.makeText(context, e.getMessage(), Toast.LENGTH_LONG).show();
            e.printStackTrace();
        }
    }

    private  Intent  Filter(){
        IntentFilter ifilter = new IntentFilter(Intent.ACTION_BATTERY_CHANGED);
        return  context.registerReceiver(null, ifilter);
    }
    public  int GetBatteryLevel(String tpye)
    {
        int level = 0;
        try
        {
            level = Filter().getIntExtra(tpye, 0);
        } catch (Exception e){ e.printStackTrace(); }
        return level;
    }
}