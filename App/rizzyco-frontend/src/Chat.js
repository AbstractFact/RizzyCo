import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

import ChatWindow from './messaging/ChatWindow';
import ChatInput from './messaging/ChatInput';

const Chat = () => {
    const [ connection, setConnection ] = useState(null);
    const [ chat, setChat ] = useState([]);
    const latestChat = useRef(null);

    latestChat.current = chat;

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44348/RizzyCoHub')
            .configureLogging(LogLevel.Debug)
            .build()

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(result => {
                    console.log('Connected!');
    
                    // connection.on('ReceiveTestMessage', message => {
                    //     const updatedChat = [...latestChat.current];
                    //     updatedChat.push(message);
                    
                    //     setChat(updatedChat);
                    // });

                    connection.on('ReceiveTestMessage', message => {
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                    
                        setChat(updatedChat);
                    });

                    // connection.on('ReceiveListMessage', message => {
                    //     const updatedChat = [...latestChat.current];
                    //     updatedChat.push(message);
                    
                    //     setChat(updatedChat);
                    // });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendMessage = async (user, message) => {
        const chatMessage = {
            user: user,
            message: message
        };
        
        if (connection.connectionStarted) {
            console.log("uso u sendMessage");
            try {
                await connection.send('SendMessage', chatMessage);
                console.log("poslao");
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            alert('No connection to server yet.');
        }


        // try {
        //     await  fetch('https://localhost:5001/chat/messages', { 
        //         method: 'POST', 
        //         body: JSON.stringify(chatMessage),
        //         headers: {
        //             'Content-Type': 'application/json'
        //         }
        //     });
        // }
        // catch(e) {
        //     console.log('Sending message failed.', e);
        // }
    }

    return (
        <div>
            <ChatInput sendMessage={sendMessage} />
            <hr />
            <ChatWindow chat={chat}/>
        </div>
    );
};

export default Chat;