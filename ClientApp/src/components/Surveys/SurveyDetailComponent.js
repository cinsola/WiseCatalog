﻿import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import SurveyPresentationComponent from './SurveyPresentationComponent'
import QuestionPresentationComponent from '../Questions/QuestionPresentationComponent';
import WithLoading from '../LoadingHOC';
class SurveyDetailComponent extends React.Component {
    constructor(props) {
        super(props);
    }

    sendMutation(questioId, value) {
        this.props.requestSurveyQuestionMutation(questioId, this.props.survey.id, value);
    }

    render() {
        return (
            <div>
                <SurveyPresentationComponent survey={this.props.survey} withLinks={false} />
                {this.props.survey.questions.map(question =>
                    <QuestionPresentationComponent key={question.id} question={question} sendMutation={(val) => this.sendMutation(question.id, val.name)} />
                )}
            </div>
        );
    }
}

const SurveyDetailComponentWithLoading = WithLoading(SurveyDetailComponent, (_context) => _context.props.requestSurvey(parseInt(_context.props.match.params.id)));
export default connect(
    state => state.surveysReducer,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(SurveyDetailComponentWithLoading);