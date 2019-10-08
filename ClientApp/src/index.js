import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

const rootElement = document.getElementById('root');

const theme = createMuiTheme({
    palette: {
        primary: {
            main: '#c2185b'
        },
        secondary: {
            main: '#cddc39'
        }
    },
    typography: {
        useNextVariants: true
    }
});

ReactDOM.render(
  <Provider store={store}>
        <ConnectedRouter history={history}>
            <MuiThemeProvider theme={theme}>
                <App />
            </MuiThemeProvider>
    </ConnectedRouter>
  </Provider>, rootElement);
console.log("Running: " + React.version);
registerServiceWorker();