import React from 'react';
import { connect } from 'react-redux';
import { surveysActionCreators } from '../../store/SurveysStore';
import WithLoading from '../LoadingHOC';
import SurveyPresentationComponent from './SurveyPresentationComponent';
import Grid from '@material-ui/core/Grid';

class SurveyCollectionComponent extends React.Component {
    render() {
        return (
            <Grid spacing={2} container direction="column">
                {this.props.surveys.map(survey =>
                    (<Grid item key={survey.id}>
                        <SurveyPresentationComponent survey={survey} />
                    </Grid>)
                )}
            </Grid>);
    }
}

const SurveysWithLoading = WithLoading(SurveyCollectionComponent, (_context) => _context.props.requestSurveys());
export default connect(state => state.surveysReducer, surveysActionCreators)(SurveysWithLoading);