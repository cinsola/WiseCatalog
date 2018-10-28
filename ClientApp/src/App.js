import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';

export default class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
            </Layout>
        );
    }
}