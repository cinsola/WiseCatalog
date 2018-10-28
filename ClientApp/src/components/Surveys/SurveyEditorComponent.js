import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import { Survey } from './SurveyComponent';

class SurveyEditorComponent extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.requestSurvey(parseInt(this.props.match.params.id));
    }

    render() {
        if (this.props.isLoading === false)
            return (
                <Survey survey={this.props.survey} withLinks={false} />
            );
        else {
            return <h1>caricamento...</h1>;
        }
    }
}

export default connect
    (
        state => state.surveysReducer,
        dispatch => bindActionCreators(actionCreators, dispatch)
    )(SurveyEditorComponent);