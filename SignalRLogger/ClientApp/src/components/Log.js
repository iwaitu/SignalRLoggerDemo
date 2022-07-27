
import React, { Component } from 'react';
import * as signalR from '@microsoft/signalr';

export class Log extends Component {
    static displayName = Log.name;

    constructor(props) {
        super(props);
        this.state = {
            messages: [],
            number: 0,
        }
    }

    componentDidMount() {
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
        
    }



    render() {
        const { messages } = this.state;
        console.log(messages);
        return (
            <div>
                <h1>Hello, world!</h1>
                <p>messages ({messages.length})</p>
                <p>
                    <ul>
                {messages.map(item =>(
                    <li>
                        {item}
                    </li>
                ))}
                    </ul>
                </p>
                <p>end ...</p>
                
            </div>
        );
    }
}