import React from 'react';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { Link } from 'react-router-dom';

export default class SurveyPresentationComponent extends React.Component {
    onSurveyChoosen() {
        if ('onSurveyChoosen' in this.props) {
            this.props.onSurveyChoosen();
        }
    }

    render() {
        const survey = this.props.survey;
        const showLinks = this.props.withlinks !== false;
        if (survey !== null) {
            return (
                    <Card>
                        <CardContent>
                            <Typography color="textSecondary" gutterBottom>Survey</Typography>
                            <Typography variant="h5" component="h2">{survey.name}</Typography>
                        </CardContent>
                        {showLinks &&
                            <CardActions>
                                <Link to={`/survey/edit/${survey.id}`} onClick={() => this.onSurveyChoosen()}>
                                    <Button size="small">Details</Button>
                                </Link>
                            </CardActions>
                        }
                    </Card>
            );
        } else {
            return <div />;
        }
    }
}