import React from 'react';
import { connect } from 'react-redux';
import SurveyCollectionComponent from './Surveys/SurveyCollectionComponent';

class Home extends React.Component {
    render() {
        return (
            <SurveyCollectionComponent />
        );
    }
}

export default connect()(Home);
