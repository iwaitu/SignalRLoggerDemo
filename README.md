# SignalRLoggerDemo
Install-Package IVilson.Utils.Logger.SignalR

## 1.Server Side

after builder.Build()

```c#
var app = builder.Build();
app.Services.AddSignalRLogger();
```

before app.Run()
```c#
app.MapHub<SignalRLoggerHub>(SignalRLoggerHub.HubUrl);

app.Run();
```

## 2.React Client Side

import signalr

```typescript
import * as signalR from '@microsoft/signalr';
```
in componentDidMount method

```typescript
let connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7244/loghub")
            .build();   

connection.on("Broadcast", (message) => {
    console.log('LOG: ', message);
    this.setState(() => {            
       this.state.messages.push(message);
       return {
          messages: this.state.messages, number: this.state.number + 1
          };
    });
});

connection.start()
    .then(() => {
        console.log('Connection started!')
        connection.send("JoinGroup", "LogMonitor").then(()=> console.log("join LogMonitor"));
    })
    .catch(err => console.log('Error while establishing connection :('));
```
