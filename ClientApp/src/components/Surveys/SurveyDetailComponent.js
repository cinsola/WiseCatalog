import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import { Survey } from './SurveyComponent';
import { SurveyQuestion } from './SurveyQuestionComponent';
import WithLoading from '../LoadingHOC';
class SurveyDetailComponent extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <Survey survey={this.props.survey} withLinks={false} />
                {this.props.survey.questions.map(question =>
                    <SurveyQuestion key={question.id} question={question} />
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