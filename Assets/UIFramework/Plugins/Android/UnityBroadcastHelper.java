package com.Unity.Tools;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.util.Log;
import java.util.LinkedList;
import java.util.Queue;



public class UnityBroadcastHelper
{
    private static final String TAG = "UnityBroadcastHelper";
    private final BroadcastListener listener;
    private Queue<String[]> keysQueue = new LinkedList<>();
    private Queue<String[]> valuesQueue = new LinkedList<>();
    private Context context;
	

    public UnityBroadcastHelper(String[] actions, BroadcastListener listener,Context Unitycontext)
    {
        Log.d(TAG, "UnityBroadcastHelper: actions: " + actions);
        Log.d(TAG, "UnityBroadcastHelper: listener: " + listener);
        this.listener = listener;
        IntentFilter intentFilter = new IntentFilter();
        for (String action : actions) {
            intentFilter.addAction(action);
        }
        if (Unitycontext == null) {
            return;
        }
        this.context =Unitycontext;
        this.context.registerReceiver(broadcastReceiver, intentFilter);
    }
    public boolean hasKeyValue() {
        return !keysQueue.isEmpty();
    }
    public String[] getKeys() {
        return keysQueue.peek();
    }
    public String[] getValues() {
        return valuesQueue.peek();
    }
    public void pop() {
        keysQueue.poll();
        valuesQueue.poll();
    }
    public void stop() {
        Context context =  this.context;
        if (context == null) {
            return;
        }
        context.unregisterReceiver(broadcastReceiver);
    }
    private BroadcastReceiver broadcastReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            Log.d(TAG, "UnityBroadcastHelper: action: " + action);
            Bundle bundle = intent.getExtras();
            if (bundle == null) {
                bundle = new Bundle();
            }
            int n = bundle.size();
            String[] keys = new String[n];
            String[] values = new String[n];
            int i = 0;
            for (String key : bundle.keySet()) {
                keys[i] = key;
                Object value = bundle.get(key);
                values[i] = value != null ? value.toString() : null;
                Log.d(TAG, "UnityBroadcastHelper: key[" + i + "]: " + key);
                Log.d(TAG, "UnityBroadcastHelper: value[" + i + "]: " + value);
                i++;
            }
            keysQueue.offer(keys);
            valuesQueue.offer(values);
            listener.onReceive(action);
        }
    };
}