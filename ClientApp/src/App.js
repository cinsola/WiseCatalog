import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import SurveyEditor from './components/Surveys/SurveyEditorComponent';
export default class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/survey/edit/:id' component={SurveyEditor} />
            </Layout>
        );
    }
}