import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import Survey from './SurveyComponent';
import WithLoading from '../LoadingHOC';
class Surveys extends React.Component {
    constructor(props) {
        super(props);
    }


    render() {
        return (
            <section>
                {this.props.surveys.map(survey =>
                    <Survey key={survey.id} survey={survey} />
                )}
            </section>);
    }
}
const SurveysWithLoading = WithLoading(Surveys, (_context) => _context.props.requestSurveys());
export default connect(state => state.surveysReducer, dispatch => bindActionCreators(actionCreators, dispatch))(SurveysWithLoading);
