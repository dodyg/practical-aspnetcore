import * as React from 'react';
const dotnetify = require('dotnetify');

class Demo extends React.Component<IDemoProp, IDemoState> {
    vm: any;

    constructor(props) {
        super(props);

        // Connect this React component to the server.
        this.vm = dotnetify.react.connect('DemoVM', this);

        // Initialize state.
        this.state = {};

        // Bind event handlers to 'this'.
        this.IncrementCounterOnServer = this.IncrementCounterOnServer.bind(this);
    }

    // This is necessary otherwise everytime you reload the application
    // there will be some instance dangling on the server and will prevent
    // others from connecting.
    componentWillUnmount() {
        this.vm.$destroy();
    }

    IncrementCounterOnServer() {
        // This is Dotnetify way of calling methods on the server VM.
        this.vm.$dispatch({ IncrementCounter: {} });
    }

    render() {
        return (
            <div>
                <div style={{ textAlign: "center" }}>
                    <h2>This page refresh automatically all data displayed every 1 second only.</h2>
                </div>

                <hr />

                <h3>Server time is: {new Date(this.state.Now).toString()}</h3>

                <h3>Counter equal to {this.state.Counter}</h3>
                <p>
                    <button onClick={this.IncrementCounterOnServer}>
                        Increment Server Counter
                    </button>
                </p>
            </div>
        );
    }
}

interface IDemoProp {
}

interface IDemoState {
    Counter?: number;
    Now?: string;
}

export default Demo;