using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static System.Console;

namespace SignalRBlazor.Console
{
  class Program
  {
    static async Task Main(string[] args)
    {
      System.Console.BackgroundColor = ConsoleColor.DarkGray;
      System.Console.ForegroundColor = ConsoleColor.Green;
      System.Console.Title = "Time";
      var connection = new HubConnectionBuilder().WithUrl("https://localhost:44373/timehub").Build();

      await connection.StartAsync();

      connection.On<string>("updateCurrentTime", (time) => {
        Clear();
        WriteLine(time);
      });

      while(connection.State == HubConnectionState.Connected)
      {

      }
    }
  }
}
