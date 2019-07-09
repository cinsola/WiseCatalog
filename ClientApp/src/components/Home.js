import React from 'react';
import { connect } from 'react-redux';
import SurveyCollectionComponent from './Surveys/SurveyCollectionComponent';

class Home extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <SurveyCollectionComponent />
            </div>
        );
    }
}

export default connect()(Home);
