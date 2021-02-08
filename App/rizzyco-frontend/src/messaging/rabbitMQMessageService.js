import SignalRService from './signalRService';

export default class rabbitMQMessageService {
    constructor(messageReceived, mqMessageReceived, testMessageReceived) {
        this._messageReceived = messageReceived;
        this._mqMessageReceived = mqMessageReceived;
        this._testMessageReceived = testMessageReceived;

        SignalRService.registerReceiveEvent((msg) => {
            this._messageReceived(msg);
        });

        SignalRService.registerReceiveMQEvent((msg) => {
            this._mqMessageReceived(msg);
        });

        SignalRService.registerReceiveTestEvent((msg) => {
            this._testMessageReceived(msg);
        });
    }

    sendMessage = (message) => {
        SignalRService.sendMessage(message);
    }
}