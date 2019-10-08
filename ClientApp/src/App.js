import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import SurveyDetailRoutedComponent from './components/Surveys/SurveyDetailRoutedComponent';
export default class App extends React.Component {

    render() {
        return (
            <Layout>
                <Route exact path='/' component={(props) => <Home {...props} />} />
                <Route path='/survey/edit/:id' component={(props) => <SurveyDetailRoutedComponent {...props} />} />
            </Layout>
        );
    }
}