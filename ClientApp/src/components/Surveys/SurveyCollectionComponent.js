import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from './SurveysStore';
import WithLoading from '../LoadingHOC';
import SurveyPresentationComponent from './SurveyPresentationComponent';
class SurveyCollectionComponent extends React.Component {
    constructor(props) {
        super(props);
    }


    render() {
        return (
            <section>
                {this.props.surveys.map(survey =>
                    <SurveyPresentationComponent key={survey.id} survey={survey} />
                )}
            </section>);
    }
}
const SurveysWithLoading = WithLoading(SurveyCollectionComponent, (_context) => _context.props.requestSurveys());
export default connect(state => state.surveysReducer, dispatch => bindActionCreators(actionCreators, dispatch))(SurveysWithLoading);
