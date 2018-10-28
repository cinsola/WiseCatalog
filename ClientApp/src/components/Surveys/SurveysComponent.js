import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import { Survey } from './SurveyComponent';
class Surveys extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.requestSurveys();
    }

    render() {
        if (this.props.isLoading === false) {
            return (<section>
                {this.props.surveys.map(survey =>
                    <Survey key={survey.id} survey={survey} />
                )}
            </section>);
        } else {
            return <h1>carimento</h1>
        }
    }
}

export default connect(state => state.surveysReducer, dispatch => bindActionCreators(actionCreators, dispatch))(Surveys);
