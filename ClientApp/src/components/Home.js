import React from 'react';
import { connect } from 'react-redux';
import Surveys from './Surveys/SurveysComponent';

class Home extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <Surveys />
            </div>
        );
    }
}

export default connect()(Home);
