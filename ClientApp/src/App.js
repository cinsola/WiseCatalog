import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import SurveyDetail from './components/Surveys/SurveyDetailComponent';
export default class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={(props) => <Home {...props} />} />
                <Route path='/survey/edit/:id' component={(props) => <SurveyDetail {...props} />} />
            </Layout>
        );
    }
}