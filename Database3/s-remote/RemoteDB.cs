using System;
using System.Threading;
using s_remote.remote;
using ArrayOfString = s_remote.remote.ArrayOfString;

namespace Axel.Database {
  /// <summary> facade to delegate the <c>IModel</c> methods. </summary>
  /// <remarks> Only one call can be pending. All methods will block the caller. </remarks>
  public class RemoteDB: IModel {
    /// <summary> server handle. </summary>
    protected ServiceSoapClient client = new ServiceSoapClient();
    /// <summary> monitor. </summary>
    protected readonly object monitor = new object();
    /// <summary> controls single access. </summary>
    protected bool pending;
    /// <summary> controls producer/consumer exchange. </summary>
    protected bool gotValue;
    /// <summary> value exchanged between producer and consumer. </summary>
    protected object value;

    /// <summary> connect event handlers once. </summary>
    public RemoteDB () {
      client.get_CountCompleted += new EventHandler<get_CountCompletedEventArgs>(get_CountDone);
      client.SearchCompleted += new EventHandler<SearchCompletedEventArgs>(SearchDone);
      client.EnterCompleted += new EventHandler<EnterCompletedEventArgs>(EnterDone);
      client.RemoveCompleted += new EventHandler<RemoveCompletedEventArgs>(RemoveDone);
    }
    /// <summary> event handler for service completion. </summary>
    protected void get_CountDone (object sender, get_CountCompletedEventArgs args) {
      lock (monitor) {
        // post value from service
        value = args.Result;
        // tell single consumer
        gotValue = true; Monitor.PulseAll(monitor);
      }
    }
    /// <summary> event handler for service completion. </summary>
    protected void SearchDone (object sender, SearchCompletedEventArgs args) {
      lock (monitor) {
        // post value from service
        value = args.Result;
        // tell single consumer
        gotValue = true; Monitor.PulseAll(monitor);
      }
    }
    /// <summary> event handler for service completion. </summary>
    protected void EnterDone (object sender, EnterCompletedEventArgs args) {
      lock (monitor) {
        // post value from service
        value = args.Result;
        // tell single consumer
        gotValue = true; Monitor.PulseAll(monitor);
      }
    }
    /// <summary> event handler for service completion. </summary>
    protected void RemoveDone (object sender, RemoveCompletedEventArgs args) {
      lock (monitor) {
        // post value from service
        value = args.Result;
        // tell single consumer
        gotValue = true; Monitor.PulseAll(monitor);
      }
    }

    public int Count {
      get {
        lock (monitor) {
          // get exclusive access
          while (pending) Monitor.Wait(monitor);
          pending = true;
          // contact service asynchronously
          client.get_CountAsync();
          // wait for value
          while (!gotValue) Monitor.Wait(monitor);
          // consume value
          int result = (int)value; gotValue = false;
          // release exclusive access
          pending = false; Monitor.Pulse(monitor);
          return result;
        }
      }
    }

    public string[][] Search (string[] keys) {
      lock (monitor) {
        // get exclusive access
        while (pending) Monitor.Wait(monitor);
        pending = true;
        // contact service asynchronously
        var send = new ArrayOfString();
        foreach (string s in keys) send.Add(s);
        client.SearchAsync(send);
        // wait for value
        while (!gotValue) Monitor.Wait(monitor);
        // consume value
        var receive = (System.Collections.ObjectModel.ObservableCollection<ArrayOfString>)value; gotValue = false;
        var result = new string[receive.Count][];
        for (int n = 0; n < result.Length; ++n)
        {
            result[n] = new string[receive[n].Count];
            for (int m = 0; m < receive[n].Count; ++m)
            {
                result[n][m] = receive[n][m];
            }
        }
        // release exclusive access
        pending = false; Monitor.Pulse(monitor);
        return result;
      }
    }

    public bool Enter (string[] tuple) {
      lock (monitor) {
        // get exclusive access
        while (pending) Monitor.Wait(monitor);
        pending = true;
        // contact service asynchronously
        var send = new ArrayOfString();
        foreach (string s in tuple) send.Add(s);
        client.EnterAsync(send);
        // wait for value
        while (!gotValue) Monitor.Wait(monitor);
        // consume value
        bool result = (bool)value; gotValue = false;
        // release exclusive access
        pending = false; Monitor.Pulse(monitor);
        return result;
      }
    }

    public bool Remove (string[] keys) {
      lock (monitor) {
        // get exclusive access
        while (pending) Monitor.Wait(monitor);
        pending = true;
        // contact service asynchronously
        var send = new ArrayOfString();
        foreach (string s in keys) send.Add(s);
        client.RemoveAsync(send);
        // wait for value
        while (!gotValue) Monitor.Wait(monitor);
        // consume value
        bool result = (bool)value; gotValue = false;
        // release exclusive access
        pending = false; Monitor.Pulse(monitor);
        return result;
      }
    }
  }
}
